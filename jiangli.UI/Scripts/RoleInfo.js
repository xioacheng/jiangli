$(document).ready(function () {
    $("#UserManager").addClass("active");
})
function addPermit(id) {
    $("#submitid").val(id);
    $('#addPermit').modal();
}
function submitData() {
    var array = [];
    var id = $("#submitid").val();
    var temp = $("input[type='checkbox']:checked");
    $(temp).each(function () {
        array.push(this.value);
    });
    $.ajax({
        url:'/role/addPermit',
        data: {
            'id':id,
            'value':array,
        },
        type: 'post',
        dataType: 'text',
        traditional: true,
        success: function (data) {
            alert(data);
        }
    })
}
function Detail(id) {
    $.ajax({
        url: '/role/permitdetail',
        data: {
            'id':id,
        },
        type:'post',
        dataType: 'json',
        success: function (data) {
            var content = $("#content");
            content.text("");
            if (data !="false") {
                $.each(data, function () {
                    content.append("<input type='checkbox' checked='checked' disabled='true'/>" + this);
                });
            }
            if(data=="false") content.append("角色暂无权限！");
            $("#detail").modal();
        }
    })
}
function addUserRole()
{
    $("#addUserRole").modal();
}
function addRole() {
    var role = $("#roleID").val();
    $.ajax({
        url: '/role/addrole',
        data: {
            'Role':role,
        },
        type: 'post',
        dataType: 'json',
        success: function (data) {
            alert(data);
            window.location.reload();
        },
    })
}
function deleteRole(id){
    $.ajax({
        url: '/Role/Delete',
        data: {
            'id':id,
        },
        type: 'post',
        dataType: 'json',
        success: function (data) {
            alert(data);
            window.location.reload();
        }
    })
}