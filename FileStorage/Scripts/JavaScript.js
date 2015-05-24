
$(function () {
    $(document).on('change', 'input[type="file"]', function () {
        $(this).parents('form').submit(); 
    });
});


$(function () {
    $(document.body).on('change', 'input[type="checkbox"]', function (e) {
        var id = $(this).attr('class');
        var postData = { id: id };
        $.ajax({
            type: "POST",
            url: '/Home/MakeOpen',
            data: postData,
            cache: false,
        });
        return false;
    });
});


$(function () {
    $("#search").keyup(function () {
        var data = { search: $('#search').val(), name: $('searchName').val() };
        $.ajax({
            type: "POST",
            url: '/Home/Search',
            data: data,
            cache: false,
            dataType: "html",
            success: function (response) {
                $(".main").html(response);
            }
        });
        return false;
    });
});

$(function () {
    jQuery(document.body).on('click', '.deleteButton', function (event) {
        var id = $(this).data("fileid");
        var postData = { id: id };
        $.ajax({
            type: "POST",
            url: '/Home/InBox',
            data: postData,
            cache: false,
            success: function (response) {
                $('tr[data-fileid=' + id + ']').remove();
                $("#trash").html(response);
            }
        });
        return false;
    });
});


$(function () {
    jQuery(document.body).on('click', '.restoreButton', function (event) {
        var id = $(this).data("fileid");
        var postData = { id: id };
        $.ajax({
            type: "POST",
            url: '/Home/Restore',
            data: postData,
            cache: false,
            success: function (response) {
                $('tr[data-fileid=' + id + ']').remove();
                $(".main").html(response);
            }
        });
        return false;
    });
});

$(function () {
    jQuery(document.body).on('click', '.delButton', function (event) {
        var id = $(this).data("fileid");
        var postData = { id: id };
        $.ajax({
            type: "POST",
            url: '/Home/DeleteFile',
            data: postData,
            cache: false,
            success: function (response) {
                $('tr[data-fileid=' + id + ']').remove();
            }
        });
        return false;
    });
});


jQuery(document.body).on('click', '.editButton', function (event) {
    var fileid = $(this).data("fileid");
    var oldName = $('td[data-fileid=' + fileid + ']').text();
    $('#saveNewName').data("fileid", fileid);
    $('#renameInput').val(oldName);
});



$(function () {
    jQuery(document.body).on('click', '#saveNewName', function (event) {
        var id = $(this).data("fileid");
        var newName = $('#renameInput').val();
        var postData = { id: id, newName: newName  };
        $.ajax({
            type: "POST",
            url: '/Home/RenameFile',
            data: postData,
            cache: false,
            success: function (response) {
                $('td[data-fileid=' + id + ']').text(response);
            }
        });
        return false;
    });
});


//jQuery(document.body).on('click', '.loadButton', function (event) {
//    var id = $(this).data("fileid");
//    var postData = { id: id };
//    $.ajax({
//        async: true,
//        type: "POST",
//        url: '/Home/Load',
//        //data: { "search": search },
//        data: postData,
//        cache: false
//    });
//    return false;
//});
