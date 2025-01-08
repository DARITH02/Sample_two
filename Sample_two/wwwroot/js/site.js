// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

const { Modal } = require("../lib/bootstrap/dist/js/bootstrap.bundle")
// Write your JavaScript code.
function edite(id) {
    $.ajax({
        url: "/Product/Edit",
        type: "GET",
        data: { id: id },
        success: function (data) {
          
            $("#Pro_name").val(data['pro_name'])
            $("#Cate_name").prepend(`<option selected hidden value='${data['cate_name']}'>${data['cate_name']}</option>`)
            $("#Status").val(data['status'])
            $("#Condition").val(data['condition'])
            $("#Id").val(data['id'])
            $("#Price").val(data['price'])
            $("#img_name").val(data['img_name'])
            $(".img_preview").attr("src", `/images/${data['img_name']}`)
        }
    })
}
function update() {
    var frm = new FormData($("#Update_form")[0]);
    var id = $("#Id").val()
    $.ajax({
        url: "/Product/Edit",
        type: "POST",
        data: frm,
        dataType: "JSON",
        processData: false,
        contentType: false,
        success: function (data) {
            console.log(data)
            Swal.fire({
                position: "center",
                icon: "success",
                title: "Your work has been saved",
                showConfirmButton: false,
                timer: 1500
            });
         /*   setTimeout(function () {
                window.location.reload()
            },1300)*/
        }
    })
}


function delete_item(id){


    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
            });
            $.ajax({
                url: "/Product/Delete",
                data: { id: id },
                type: "POST",

                success: function (data) {
                    console.log(data)
                    if (data == 200) {

                        setTimeout(function () { window.location.reload() }, 1500)
                    }
                }
            })
        }
    });

}

function viewDetail(id) {
    $.ajax({
        url: "/Product/ViewDetail",
        data: { id: id },
        type: "GET",
        success: function (data) {
            console.log(data)
            var tr = ""
            tr =`
              <tr>
                <td class="text-end  px-2" style="width:150px">Id</td>
                    <td>${data["id"]}</td>
                </tr> <tr>
                        <td class="text-end  px-2" style="width:150px">Products Name</td>
                    <td>${data["pro_name"]}</td>
                </tr> <tr>
                        <td class="text-end  px-2" style="width:150px">Category Name</td>
                    <td>${data["cate_name"]}</td>
                </tr> <tr>
                        <td class="text-end  px-2" style="width:150px">Price</td>
                    <td>${data["price"]}</td>
                </tr> <tr>
                        <td class="text-end  px-2" style="width:150px">Status</td>
                    <td>${data["status"]}</td>
                </tr> <tr>
                        <td class="text-end  px-2" style="width:150px">Condition</td>
                    <td>${data["condition"]}</td>
                </tr>
            `; 

            $("#tbl_view").html(tr)


        }
    })
}