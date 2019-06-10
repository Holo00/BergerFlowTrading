using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace BergerFlowTrading.Shared.DTO.Trading
{
    public class Observable<T> where T : UniqueIDItem, ICloneable
    {
        private SemaphoreSlim sem { get; set; }

        private T value { get; set; }

        public Observable(T value)
        {
            this.sem = new SemaphoreSlim(1, 1);
            this.value = value;
        }

        public T Value
        {
            get
            {
                try
                {
                    this.sem.WaitAsync().Wait();
                    return (T)Value.Clone();

                }
                finally
                {
                    this.sem.Release();
                }
            }
            set
            {
                try
                {
                    this.sem.WaitAsync().Wait();
                    this.value = value;

                }
                finally
                {
                    this.sem.Release();
                }
            }
        }

        public void Update(T newValues)
        {
            if (this.value == null)
            {
                this.value = newValues;
            }
            else
            {
                var props = typeof(T).GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    object val = prop.GetValue(newValues, null);

                    if (val != null)
                    {
                        prop.SetValue(this.Value, val);

                    }
                }
            }
        }
    }

    public class ObservableList<T> where T : UniqueIDItem, ICloneable
    {
        private SemaphoreSlim sem { get; set; }

        private List<T> value { get; set; }

        public ObservableList(List<T> value)
        {
            this.sem = new SemaphoreSlim(1, 1);
            this.value = value;
        }

        public List<T> Value
        {
            get
            {
                try
                {
                    this.sem.WaitAsync().Wait();
                    return this.value.Select(item => (T)item.Clone()).ToList();

                }
                finally
                {
                    this.sem.Release();
                }
            }
            set
            {
                try
                {
                    this.sem.WaitAsync().Wait();
                    this.value = value;

                }
                finally
                {
                    this.sem.Release();
                }
            }
        }

        public void Add(T t)
        {
            this.value.Add(t);
        }

        public void Remove(T t)
        {
            T item = this.value.FirstOrDefault(x => x.UniqueID == t.UniqueID);
            this.value.Remove(item);
        }

        public void Update(T newValues)
        {
            T t = this.value.FirstOrDefault(x => x.UniqueID == newValues.UniqueID);

            if (t == null)
            {
                this.value.Add(newValues);
            }
            else
            {
                this.UpdateValue(t, newValues);
            }
        }

        private void UpdateValue(T t, T newValues)
        {
            var props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object val = prop.GetValue(newValues, null);

                if (val != null)
                {
                    prop.SetValue(t, val);

                }
            }
        }
    }
}
