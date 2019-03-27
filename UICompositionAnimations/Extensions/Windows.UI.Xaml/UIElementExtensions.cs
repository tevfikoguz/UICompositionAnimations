﻿using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Xaml.Hosting;
using JetBrains.Annotations;
using UICompositionAnimations;
using UICompositionAnimations.Animations;
using UICompositionAnimations.Animations.Interfaces;
using UICompositionAnimations.Enums;

namespace Windows.UI.Xaml
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
        [Pure, NotNull]
        public static IAnimationBuilder Animate([NotNull] this UIElement target)
        {
            return new CompositionAnimationBuilder(target);
        }

        /// <summary>
        /// Stops the animations with the target names on the given <see cref="UIElement"/>
        /// </summary>
        /// <param name="element">The target <see cref="UIElement"/></param>
        /// <param name="properties">The names of the animations to stop</param>
        public static void StopAnimations([NotNull] this UIElement element, [NotNull, ItemNotNull] params string[] properties)
        {
            if (properties.Length == 0) return;
            Visual visual = element.GetVisual();
            foreach (string property in properties)
                visual.StopAnimation(property);
        }

        /// <summary>
        /// Sets the <see cref="Visual.Offset"/> property of the <see cref="Visual"/> instance for a given <see cref="UIElement"/> object
        /// </summary>
        /// <param name="element">The target <see cref="UIElement"/></param>
        /// <param name="axis">The <see cref="Visual.Offset"/> axis to edit</param>
        /// <param name="offset">The <see cref="Visual.Offset"/> value to set for that axis</param>
        public static void SetVisualOffset([NotNull] this UIElement element, TranslationAxis axis, float offset)
        {
            // Get the element visual and stop the animation
            Visual visual = element.GetVisual();

            // Set the desired offset
            Vector3 offset3 = visual.Offset;
            if (axis == TranslationAxis.X) offset3.X = offset;
            else offset3.Y = offset;
            visual.Offset = offset3;
        }

        /// <summary>
        /// Sets the translation property of the <see cref="Visual"/> instance for a given <see cref="UIElement"/> object
        /// </summary>
        /// <param name="element">The target <see cref="UIElement"/></param>
        /// <param name="axis">The <see cref="Visual.Offset"/> axis to edit</param>
        /// <param name="translation">The translation value to set for that axis</param>
        public static void SetVisualTranslation([NotNull] this UIElement element, TranslationAxis axis, float translation)
        {
            // Get the element visual and stop the animation
            Visual visual = element.GetVisual();
            ElementCompositionPreview.SetIsTranslationEnabled(element, true);

            // Set the desired offset
            Matrix4x4 transform = visual.TransformMatrix;
            Vector3 translation3 = visual.TransformMatrix.Translation;
            if (axis == TranslationAxis.X) translation3.X = translation;
            else translation3.Y = translation;
            transform.Translation = translation3;
            visual.TransformMatrix = transform;
        }

        /// <summary>
        /// Sets the <see cref="Visual.Scale"/> property of the <see cref="Visual"/> instance for a given <see cref="UIElement"/>
        /// </summary>
        /// <param name="element">The target <see cref="UIElement"/></param>
        /// <param name="x">The X value of the <see cref="Visual.Scale"/> property</param>
        /// <param name="y">The Y value of the <see cref="Visual.Scale"/> property</param>
        /// <param name="z">The Z value of the <see cref="Visual.Scale"/> property</param>
        public static void SetVisualScale([NotNull] this UIElement element, float? x, float? y, float? z)
        {
            // Get the default values and set the CenterPoint
            Visual visual = element.GetVisual();

            // Set the scale property
            if (x == null && y == null && z == null) return;
            Vector3 targetScale = new Vector3
            {
                X = x ?? visual.Scale.X,
                Y = y ?? visual.Scale.Y,
                Z = z ?? visual.Scale.Z
            };
            visual.Scale = targetScale;
        }
    }
}
