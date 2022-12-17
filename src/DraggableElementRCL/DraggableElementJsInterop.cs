using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraggableElementRCL;
public interface IDraggableElementJsInterop
{
    Task Initial(ElementReference draggableElementReference, DotNetObjectReference<DraggableElement>? dotNetHelper, float left, float top);
}
internal class DraggableElementJsInterop : IAsyncDisposable, IDraggableElementJsInterop
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;
    public DraggableElementJsInterop(IJSRuntime jSRuntime)
    {
        moduleTask = new(() => jSRuntime.InvokeAsync<IJSObjectReference>(
           "import", "./_content/DraggableElementRCL/mainJsInterop.js").AsTask());
    }

    public async Task Initial(ElementReference draggableElementReference, DotNetObjectReference<DraggableElement>? dotNetHelper, float left, float top)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("dragElementInitial", draggableElementReference, top, left, dotNetHelper);
    }
    public async ValueTask DisposeAsync()
    {
        if (moduleTask != null)
        {
            moduleTask.Value.Dispose();
        }
    }
}
