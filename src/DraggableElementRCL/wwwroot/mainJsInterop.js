// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

export function showPrompt(message) {
    return prompt(message, 'Type anything here');
}
export function dragElementInitial(elmnt, top, left, dotNetHelper) {
    var pos1 = 0, pos2 = 0, pos3 = 0, pos4 = 0;
    if (elmnt) {
        /* otherwise, move the DIV from anywhere inside the DIV:*/
        elmnt.onmousedown = dragMouseDown;

        if (left && left > 0)
            elmnt.style.left = left + "px";

        if (top && top > 0)
            elmnt.style.top = top + "px";
    }

    function dragMouseDown(e) {
        e = e || window.event;
        e.preventDefault();
        // get the mouse cursor position at startup:
        pos3 = e.clientX;
        pos4 = e.clientY;
        document.onmouseup = closeDragElement;
        // call a function whenever the cursor moves:
        document.onmousemove = elementDrag;
    }

    function elementDrag(e) {
        e = e || window.event;
        e.preventDefault();
        // calculate the new cursor position:
        pos1 = pos3 - e.clientX;
        pos2 = pos4 - e.clientY;
        pos3 = e.clientX;
        pos4 = e.clientY;
        // set the element's new position:
        if ((elmnt.offsetTop - pos2) < 0)
            elmnt.style.top = "0";
        else
            elmnt.style.top = (elmnt.offsetTop - pos2) + "px";

        if ((elmnt.offsetLeft - pos1) < 0)
            elmnt.style.left = "0";
        else
            elmnt.style.left = (elmnt.offsetLeft - pos1) + "px";

    }

    function closeDragElement() {
        /* stop moving when mouse button is released:*/
        dotNetHelper.invokeMethodAsync('CloseDragElement', elmnt.style.top, elmnt.style.left);
        document.onmouseup = null;
        document.onmousemove = null;
    }
}