//$(function () {
//    $(document).ready(function () {
//        var toggler = document.getElementsByClassName("caret");
//        var i;

//        for (i = 0; i < toggler.length; i++) {
//            toggler[i].addEventListener("click", function () {
//                this.parentElement.querySelector(".nested").classList.toggle("active");
//                this.classList.toggle("caret-down");
//            });
//        }
//    });
//});

function UnHide(eThis) {
    if (eThis.innerHTML.charCodeAt(0) == 9658) {
        eThis.innerHTML = '&#9660;'
        eThis.parentNode.parentNode.parentNode.className = '';
    } else {
        eThis.innerHTML = '&#9658;'
        eThis.parentNode.parentNode.parentNode.className = 'cl';
    }
    return false;
}
