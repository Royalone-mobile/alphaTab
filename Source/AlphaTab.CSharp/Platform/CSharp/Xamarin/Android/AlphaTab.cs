﻿#if ANDROID
/*
 * This file is part of alphaTab.
 * Copyright © 2017, Daniel Kuschny and Contributors, All rights reserved.
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3.0 of the License, or at your option any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library.
 */
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using AlphaTab.Collections;
using AlphaTab.Model;
using AlphaTab.Rendering;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Widget;
using SkiaSharp;

namespace AlphaTab.Platform.CSharp.Xamarin.Android
{
    public class AlphaTab : ScrollView
    {
        private AlphaTabLayoutPanel _layoutPanel;
        private Settings _settings;
        private IEnumerable<Track> _tracks;

        public IEnumerable<Track> Tracks
        {
            get { return _tracks; }
            set
            {
                if (_tracks == value) return;

                var observable = _tracks as INotifyCollectionChanged;
                if (observable != null)
                {
                    observable.CollectionChanged -= OnTracksChanged;
                }

                _tracks = value;

                observable = _tracks as INotifyCollectionChanged;
                if (observable != null)
                {
                    observable.CollectionChanged += OnTracksChanged;
                }
                RenderTracks();
            }
        }
        public Settings Settings
        {
            get => _settings;
            set
            {
                if (_settings == value) return;
                _settings = value;
                OnSettingsChanged(value);
            }
        }

        public AlphaTabApi<AlphaTab> Api { get; private set; }

        public AlphaTab(Context context)
            : base(context)
        {
            Initialize(context);
        }

        public AlphaTab(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Initialize(context);
        }

        private void Initialize(Context context)
        {
            _layoutPanel = new AlphaTabLayoutPanel(context);
            AddView(_layoutPanel);

            Settings = Settings.Defaults;
            Settings.EnablePlayer = true;
            Settings.EnableCursor = true;

            Api = new AlphaTabApi<AlphaTab>(new AndroidUiFacade(this, _layoutPanel), this);
        }

        private void OnTracksChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RenderTracks();
        }

        public void RenderTracks()
        {
            if (Tracks == null) return;

            Score score = null;
            var trackIndexes = new FastList<int>();
            foreach (var track in Tracks)
            {
                if (score == null)
                {
                    score = track.Score;
                }

                if (score == track.Score)
                {
                    trackIndexes.Add(track.Index);
                }
            }

            if (score != null)
            {
                Api.RenderTracks(score, trackIndexes.ToArray());
            }
        }

        public event Action<Settings> SettingsChanged;
        private void OnSettingsChanged(Settings obj)
        {
            SettingsChanged?.Invoke(obj);
        }
    }
}
#endif