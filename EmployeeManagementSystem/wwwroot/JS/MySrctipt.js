function DeleteConfirm(uniqueId, isConfirmBtnClick) {
    var deleteBtnId = "deleteBtn_" + uniqueId;
    var confirmBtnId = "DeleteConfirmBtn_" + uniqueId;

    if (isConfirmBtnClick) {
        $("#" + deleteBtnId).hide();
        $("#" + confirmBtnId).show();
    }
    else {
        $("#" + deleteBtnId).show();
        $("#" + confirmBtnId).hide();
    }
}