$(document).ready(function () {

    loadroomphotos('a.thumbnail');

    
    /**
     * @param setClickAttr  Sets the attribute for the click handler.
     */
    function loadroomphotos( setClickAttr) {
        var selector,
            counter = 0;

        $('[data-image-id]').each(function () {
            counter++;
            $(this).attr('data-image-id', counter);
        });

        $(setClickAttr).on('click', function () {
            updatemodalphoto($(this));
        });
        
    }

    function updatemodalphoto(selector) {
        var $sel = selector;
        //current_image = $sel.data('image-id');
        //$('#image-gallery-title').text($sel.data('title'));
        $('#image-room-caption').text($sel.data('caption'));
        $('#image-room-image').attr('src', $sel.data('image'));
    }


});
