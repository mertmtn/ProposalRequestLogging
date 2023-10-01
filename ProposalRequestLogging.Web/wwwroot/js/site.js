function createDynamicTable(elementId, dataList, titleOfColumnList) {
    var table = $("<table class='table table-striped'/>");

    var columns = Object.keys(dataList[0]);

    var row = $(table[0].insertRow(-1));
    for (var i = 0; i < titleOfColumnList.length; i++) {
        var headerCell = $("<th />");
        headerCell.html(titleOfColumnList[i]);
        row.append(headerCell);
    }

    for (var i = 0; i < dataList.length; i++) {
        row = $(table[0].insertRow(-1));
        for (var j = 0; j < columns.length; j++) {
            var cell = $("<td />");
            cell.html(dataList[i][columns[j]]);
            row.append(cell);
        }
    }

    var tableDiv = $(elementId);
    tableDiv.html("");
    tableDiv.append(table);
}

function formSubmit() {
    $("#errorMessage").hide();
    $("#successMessage").hide();

    var isValidProductNo = validateInput($("#productNo").val(), "productNo")
    var isValidRenewalNo = validateInput($("#renewalNo").val(), "renewalNo")
    var isValidProposalNo = validateInput($("#proposalNo").val(), "proposalNo")
    var isValidEndorsNo = validateInput($("#endorsNo").val(), "endorsNo")
    var isValidForm = isValidProductNo && isValidRenewalNo && isValidProposalNo && isValidEndorsNo;

    if (isValidForm) {
        
        var request = {
            Object: {}
        }

        var proposal = {}
        proposal.ProductNo = $("#productNo").val();
        proposal.ProposalNo = $("#proposalNo").val();
        proposal.RenewalNo = $("#renewalNo").val();
        proposal.EndorsNo = $("#endorsNo").val();

        request.Object = proposal;
        $.ajax({
            url: "/Proposal/Index",
            type: "POST",
            data: { request: request },
            success: function (data) {
                if (data.isSuccess) {
                    $("#successMessage").html("Sorgulama başarılıdır. Sonuçlar aşağıdaki tablolarda bulunmaktadır.").show();
                    var titleOfColumns = new Array("Kod", "Açıklama");
                    createDynamicTable("#tblOlumlu", data.positiveResultList, titleOfColumns);
                    createDynamicTable("#tblBilgi", data.informativeResultList, titleOfColumns);
                    createDynamicTable("#tblOlumsuz", data.negativeResultList, titleOfColumns);
                }
                else {
                    $("#errorMessage").html(data.errorMessage).show();
                }
            },
            error: function(data) {
                alert(data.errorMessage)
            }
        });
       
    }
}

function validateInput(validateValue, elementName) {
    if (validateValue == "") {
        document.getElementById(elementName + "Invalid").style.display = "block";
        return false;
    }
    else {
        document.getElementById(elementName + "Invalid").style.display = "none";
        return true;
    }
}