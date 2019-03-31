﻿using System;
using Windows.UI.Xaml;
using JetBrains.Annotations;
using UICompositionAnimationsLegacy.Animations;
using UICompositionAnimationsLegacy.Animations.Interfaces;
using UICompositionAnimationsLegacy.Enums;

namespace UICompositionAnimationsLegacy.Helpers
{
    /// <summary>
    /// An extension <see langword="class"/> for the <see cref="UIElement"/> <see langword="class"/>
    /// </summary>
    [PublicAPI]
    public static class UIElementExtensions
    {
        /// <summary>
        /// Initializes an <see cref="IAnimationBuilder"/> instance that targets the input <see cref="UIElement"/>
        /// </summary>
        /// <param name="target">The target <see cref="UIElement"/> to animate</param>
        /// <param name="layer">The target layer to animate</param>
        [Pure, NotNull]
        public static IAnimationBuilder Animation([NotNull] this UIElement target, FrameworkLayer layer = FrameworkLayer.Composition)
        {
            switch (layer)
            {
                case FrameworkLayer.Composition: return new CompositionAnimationBuilder(target);
                case FrameworkLayer.Xaml: return new XamlAnimationBuilder(target);
                default: throw new ArgumentOutOfRangeException(nameof(layer), layer, $"The {layer} value isn't valid");
            }
        }
    }
}
