using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BergerFlowTrading.Shared.Storage
{
    public class LocalStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }


        public Task SetLocalStorageValue<T>(string key, T value)
        {
            return _jsRuntime.InvokeAsync<object>("wasmHelper.setLocalStorage", new object[] { key, value });
        }
        public Task<T> GetLocalStorageValue<T>(string key)
        {
            return _jsRuntime.InvokeAsync<T>("wasmHelper.getLocalStorage", key);
        }

        public Task RemoveLocalStorageValue(string key)
        {
            return _jsRuntime.InvokeAsync<object>("wasmHelper.removeLocalStorage", key);
        }
    }
}
