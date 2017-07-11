$(document).ready(function () {
    $("#UserManager").addClass("active");
});
function Message(id, cell) {
    $("#myModalLabel").text("修改角色");
    $("#userid").text("账户：" + cell);
    $("#id").val(id);
    $.ajax({
        url: '/role/GetAllRole',
        data: {
        },
        type: 'post',
        dataType: 'json',
        success: function (data) {
            $("#selectId").empty();
            $.each(data, function () {
                $("#selectId").append("<input type='radio' value='"+this.RoleID+"' />"+this.RoleName);
            });
        }
    }),
    $('#myModal').modal();
}
function ModifyRole(id) {
    var value = $("input:radio:checked").val();
    var id = $("#id").val();
    $.ajax({
        url: '/user/ModifiedRole',
        data: {
            'id': id,
            'value': value
        },
        type: 'post',
        dataType: 'json',
        success: function (data) {
            window.location.reload();//刷新页面
        }
    })
}
function Info(id) {
    $.ajax({
        url: '/user/GetAllInfo',
        data: {
            'id':id,
        },
        type: 'post',
        dataType: 'Json',
        success: function (data) {
            $("#account").val(data.Account);
            $("#nickname").val(data.nickName);
            $("#weight").val(data.Weight);
            $("#numberofcase").val(data.NumberOfCase);
            $("#phone").val(data.cellPhone);
            $("#datelast").val(ChangeDateFormat(data.DateOfLast));
            $("#allMessage").modal();
        },
    })
}
//改变日期格式
function ChangeDateFormat(cellval)
{
    var date = new Date(parseInt(cellval.replace("/Date(", "").replace(")/", ""), 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    return date.getFullYear() + "-" + month + "-" + currentDate;
}
