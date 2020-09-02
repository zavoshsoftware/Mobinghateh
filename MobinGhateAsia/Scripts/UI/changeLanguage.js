function languageChanged(langInput) {
    setCookie("MyLanguageCookieName", langInput, 365, null);
    window.location.reload();
}
$(document).ready(function () {
    var cultureVal = getCookie("MyLanguageCookieName");
    if (cultureVal.includes("fa")) {
        $('#iran-flag').css('display', 'block');
        $('#us-flag').css('display', 'block');
        $('#iran-flag').css('float', 'right');
        $('#us-flag').css('float', 'right');
        $('#us-flag').css('opacity', '0.5');
        $('#iran-flag').css('margin', '3px');
        $('#us-flag').css('margin', '3px');
        document.getElementById('style-rtl').disabled = false;
        //document.getElementById('bootstrap-rtl').disabled = false;
        //document.getElementById('ltrStyle').disabled = true;
        //document.getElementById('ltrStyle2').disabled = true;

        //document.getElementById('rtlStyle3').disabled = false;
        //document.getElementById('ltrStyle3').disabled = true;

        //$('#menubox').removeClass('top-left');
        //$('#menubox').addClass('top-right');

        //$('#pro-icon').removeClass('fa-angle-right');
        //$('#pro-icon').addClass('fa-angle-left');

        //$('.blog-readmore-icon').removeClass('fa-angle-right');
        //$('.blog-readmore-icon').addClass('fa-angle-left');

    }
    else {
        $('#iran-flag').css('display', 'block');
        $('#us-flag').css('display', 'block');
        $('#iran-flag').css('float', 'right');
        $('#us-flag').css('float', 'right');
        $('#iran-flag').css('opacity', '0.5');
        $('#iran-flag').css('margin', '3px');
        $('#us-flag').css('margin', '3px');
        $('.slogan-title').removeClass('text-right');
        document.getElementById('style-rtl').disabled = true;
        document.getElementById('bootstrap-rtl').disabled = true;


        //document.getElementById('rtlStyle').disabled = true;
        //document.getElementById('rtlStyle2').disabled = true;
        //document.getElementById('ltrStyle').disabled = false;
        //document.getElementById('ltrStyle2').disabled = false;
        //document.getElementById('rtlStyle3').disabled = true;
        //document.getElementById('ltrStyle3').disabled = false;

        //$('.slogan-title').addClass('top-left');

        //$('#pro-icon').removeClass('fa-angle-left');
        //$('#pro-icon').addClass('fa-angle-right');

        //$('.blog-readmore-icon').removeClass('fa-angle-left');
        //$('.blog-readmore-icon').addClass('fa-angle-right');
    }
});

function setCookie(name, value, exdays, path) {
    var today = new Date();
    var expiry = new Date(today.getTime() + 30 * 24 * 3600 * 1000); // plus 30 days
    document.cookie = name + "=" + value + "; path=/; expires=" + expiry.toGMTString();
    //var exdate = new Date();
    //exdate.setDate(exdate.getDate() + exdays);
    //var newValue = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString()) + ((path == null) ? "" : "; path=" + path);
    //document.cookie = name + "=" + newValue;
}
function getCookie(name) {
    var i, x, y, cookies = document.cookie.split(";");
    for (i = 0; i < cookies.length; i++) {
        x = cookies[i].substr(0, cookies[i].indexOf("="));
        y = cookies[i].substr(cookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x === name) {
            return unescape(y);
        }
    }
}