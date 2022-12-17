using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace DraggableElementRCL
{
    public partial class DraggableElement
    {
        [Parameter] public RenderFragment? ElementContent { get; set; }

        [Parameter] public string Class { get; set; } = "";
        [Parameter] public string Style { get; set; } = "";

        [Parameter] public float ElementPositionTop { get; set; } = 0;
        [Parameter] public EventCallback<float> ElementPositionTopChanged { get; set; }

        [Parameter] public float ElementPositionLeft { get; set; } = 0;
        [Parameter] public EventCallback<float> ElementPositionLeftChanged { get; set; }

        [Parameter] public EventCallback OnCloseDragElement { get; set; }

        private DotNetObjectReference<DraggableElement>? dotNetHelper;
        ElementReference draggableElementReference;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                dotNetHelper = DotNetObjectReference.Create(this);
                await draggableElementJsInterop.Initial(draggableElementReference: draggableElementReference, dotNetHelper: dotNetHelper, left: ElementPositionLeft, top: ElementPositionTop);
            }
        }

        [JSInvokable]
        public async void CloseDragElement(string top, string left)
        {
            float elementTop = float.Parse(top.Replace("px", ""));
            float elementLeft = float.Parse(left.Replace("px", ""));
            await ElementPositionTopChanged.InvokeAsync(elementTop);
            await ElementPositionLeftChanged.InvokeAsync(elementLeft);

            await OnCloseDragElement.InvokeAsync();
        }
    }
}