using System.ComponentModel;
using BlazorServer_UI.Entities;
using Microsoft.AspNetCore.Components;

namespace BlazorServer_UI.Components
{
    public class ProductService : ComponentBase
    {
        [Parameter] public Product? ShopProduct { get; set; } = null;
        [Parameter] public int MaxWidth { get; set; } = 300;

        public string GetWidthParametr() => MaxWidth.ToString() + "px";
    }
}
