$(document).ready(function () {
    $("#CaseManager").addClass("active");
});
function Message(id){
    $("#myModalLabel").text("修改案件状态");
    $("#userid").text("账户");
    $("#id").val(id);
    $('#myModal').modal();
}
function ModifyState(){
    var value = $("input:radio:checked").val();
    var id = $("#id").val();
    $.ajax({
        url:'/case/modifystate',
        data:{
            'id':id,
            'value':value
        },
        type:'post',
        dataType:'json',
        success:function(data){
            window.location.reload();//刷新页面
        }
    })
}
function Delete(id){
    $.ajax({
        url:'/case/delete',
        data:{
            'deleteid':id
        },
        type:'post',
        dataType:'Json',
        success:function(data){
            if(data==true){
                alert("删除成功");
                window.location.reload();
            }
            else{
                alert("删除失败，请重试");
            }
        }
    })
}
function Detail(id, title) 
{
    $.ajax({
        url: '/case/casedetail',
        data: {
            'id':id,
        },
        type:'post',
        dataType: 'json',
        success: function (data) {
            $("#caseText").text(data);
            $("#CaseModalLabel").text(title + ":案件详情");
            $("#caseid").val(id);
            $('#caseModal').modal();
        }
    })
}