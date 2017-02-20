$(function () {

//    var videoTitle;
//    var videoUrl;

//    $('.show-video').click(function () {
//        videoTitle = $(this).attr('video-title');
//        videoUrl = $(this).attr('video-url');
//        $('#videoTitle').text(videoTitle);
//    });

//    $('#videoModal').on('show', function () {
//        alert(videoUrl);
//        var iframe = $(this).children('div.modal-body').find('iframe');
//        var src = iframe.attr('src');
//        iframe.attr('src', videoUrl);
//        iframe.attr('src', src);
//    });

//    $('#videoModal').bind('hide', function () {
//        var iframe = $(this).children('div.modal-body').find('iframe');
//        var src = iframe.attr('src');
//        iframe.attr('src', '');
//        iframe.attr('src', src);
//    });


    $('.show-video').click(function () {
        //var src = 'http://www.youtube.com/embed/KVu3gS7iJu4?autoplay=1';
        //$('#videoModal').modal('show');
        var videoTitle = $(this).attr('video-title');
        var videoUrl = $(this).attr('video-url') + '?autoplay=1&rel=0';
        $('#videoTitle').text(videoTitle);
        $('#videoModal iframe').attr('src', videoUrl);
    });

    $('#videoModal button').click(function () {
        $('#videoModal iframe').removeAttr('src');
    });

    $('#btnPrintCoupon').click(function (e) {
        window.print();
    });

    //If user presses 'Enter' while on FastAd
    //input, then fire btnGoToAd's click event
    $('input[id$=txtFastAd]').keydown(function (e) {
        if (e.keyCode == 13) {
            $('input[id$=btnGoAd]').click();
            return false;
        }
    });

});