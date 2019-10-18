using System;
using System.Collections.Generic;
using Entitas;
using UIDataBind.Base.Components;
using UIDataBind.Base.Extensions;

namespace UIDataBind.Entitas.Features.Presentation
{
    public sealed class ConverterSystem<T0, T1> : IInitializeSystem, ICleanupSystem
    {
        private readonly UiBindContext _context;
        private readonly List<UiBindEntity> _entities;

        private readonly IAllOfMatcher<UiBindEntity> _matcher0;
        private readonly IAllOfMatcher<UiBindEntity> _matcher1;
        private readonly Func<T0, T1> _t0Tot1;
        private readonly Func<T1, T0> _t1Tot0;
        private readonly int _index0;
        private readonly int _index1;

        private IGroup<UiBindEntity> _group0;
        private IGroup<UiBindEntity> _group1;

        public ConverterSystem(UiBindContext context, Func<T0, T1> t0Tot1, Func<T1, T0> t1Tot0)
        {
            _context = context;
            _t0Tot1 = t0Tot1;
            _t1Tot0 = t1Tot0;
            var engine = context.GetEngine();
            _matcher0 = UiBindMatcher.AllOf(_index0 = engine.GetPropertyIndex<T0>());
            _matcher1 = UiBindMatcher.AllOf(_index1 = engine.GetPropertyIndex<T1>());
            _entities = new List<UiBindEntity>();
        }

        public void Initialize()
        {
            _group0 = _context.GetGroup(UiBindMatcher.AllOf(UiBindMatcher.ConvertTo, _matcher0));
            _group1 = _context.GetGroup(UiBindMatcher.AllOf(UiBindMatcher.ConvertTo, _matcher1));
        }

        public void Cleanup()
        {
            TryConvert(_group0, _index0, _t0Tot1);
            TryConvert(_group1, _index1, _t1Tot0);
        }

        private void TryConvert<TSource, TTarget>(IGroup<UiBindEntity> group, int componentIndex,
            Func<TSource, TTarget> conversion)
        {
            if (group.count == 0)
                return;

            var conversionType = typeof(TTarget);
            group.GetEntities(_entities);
            foreach (var entity in _entities)
            {
                if (entity.hasConverted || entity.convertTo.Value != conversionType)
                    continue;

                var value = ((IPropertyComponent<TSource>) entity.GetComponent(componentIndex)).Value;
                entity.ReplaceConverted(conversion.Invoke(value));
                entity.RemoveConvertTo();
            }

            _entities.Clear();
        }
    }
}