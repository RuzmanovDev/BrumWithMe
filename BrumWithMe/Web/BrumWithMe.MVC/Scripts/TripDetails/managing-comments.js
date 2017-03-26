function appendNewCommentsToDom(result) {
    $("#comments section.comment-container").last().after(result);
}

function getNextPage() {
    var page = +$("#comment-page-number-hidden").val();
    page++;
    $("#comment-page-number-hidden").val(page);
}