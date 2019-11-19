function ConfirmDelete(uniqueRoleID, isDeleteClick) {
    var deleteBtnId = "deleteBtn_" + uniqueRoleID;
    var noDeleteBtnId = "noDeleteBtn_" + uniqueRoleID;
    if (isDeleteClick) {
        $("#" + deleteBtnId).hide();
        $("#" + noDeleteBtnId).show();
    }
    else {
        $("#" + deleteBtnId).show();
        $("#" + noDeleteBtnId).hide();
    }
}