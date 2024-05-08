// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))


var options = {
    html: true,
    //html element
    //content: $('[data-name="popover-content"]'),
    //content: function () {
    //    return $('[data-name="popover-content"]').html();
    //},
    title: function () {
        return title.html();
    },
    trigger: 'hover',
}


const popoverTriggerList = document.querySelectorAll('[data-bs-toggle="popover"]')
const popoverList = [...popoverTriggerList].map(popoverTriggerEl => new bootstrap.Popover(popoverTriggerEl, options))

const popover = new bootstrap.Popover('.popover-dismiss', {
    content: $('[data-name="popover-content"]'),
    trigger: 'hover',

})
    popover.update();
