﻿$(document).ready(function () {
    animateDiv();

});

function makeNewPosition() {

    // Get viewport dimensions (remove the dimension of the div)
    var h = $(window).height() - 30;
    var w = $(window).width() - 30;

    var nh = Math.floor(Math.random() * h);
    var nw = Math.floor(Math.random() * w);

    return [nh, nw];

}

function animateDiv() {
    var newq = makeNewPosition();
    $('.movingObject').animate({ top: newq[0], left: newq[1] }, function () {
        animateDiv();
    });

};