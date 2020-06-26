var takeInventoryUrl = 'https://localhost:44388/';
var viewInventoryUrl = 'https://localhost:44388/ViewInventoryDetails#';

$(document).on('click', '#submitButton', function () {
    let takeInventoryForm = $("#takeInventoryForm");
    $.ajax({
        url: 'https://localhost:44388/viewInventory/submit',
        data: takeInventoryForm.serialize(),
        type: 'POST'
    });
});

$(document).on('click', '#searchButtonTest', function () {
    if (validateSearchForm()) {
        let takeInventoryForm = $('#takeInventoryForm');
        $.ajax({
            url: 'https://localhost:44388/viewInventory/searchInventory',
            data: takeInventoryForm.serialize(),
            type: 'POST',
            success: function (response) {
                $("#renderPartialDiv").html(response);
            }
        });
    }
});

$(document).on('click', '#searchButton', function () {
    let viewInventoryForm = $("#viewInventoryForm");
    $.ajax({
        url: 'https://localhost:44388/viewInventoryDetails/searchInventoryDetails',
        data: viewInventoryForm.serialize(),
        type: 'POST',
        success: function (response) {
            $("#renderPartialDiv").html(response);
        }
    });
});

$(document).on('click', '#cancelButton', function () {
    location.reload(true);
});

$(document).on('click', '#addItem', function () {
    assetTagValue = $('#assetTag').val();
});


$('#addItemModal').bind('show.bs.modal', function () {
    $('#assetTagModalInput').val($('#assetTag').val());
    $('#inventoryOwnerModalInput').val($('#inventoryOwner').val());
    $('#locationSelectModalSelect').val($('#locationSelect').val());
    $('#clientSiteSelectModalSelect').val($('#clientSiteSelect').val());
});

$(document).on('click', '#itemFoundSubmitButton', function () {
    var tds = $('.selected').find('td');
    if (tds.length > 0) {
        $.ajax({
            url: 'https://localhost:44388/viewInventory/submitAsset',
            data: { AssetTag: tds[0].innerHTML },
            type: 'POST',
            success: function (response) {
                resetSearchValues();
                $('#renderPartialDiv').html(response);
            }
        });
    }
});


$(document).on('click', '#modifyButton', function () {
    var tds = $('.selected').find('td');
    if (tds.length > 0) {
        $.ajax({
            url: 'https://localhost:44388/viewInventory/getModalData',
            data: { id: tds[0].innerHTML },
            type: 'POST',
            success: function (response) {
                $('#renderModifyAssetModalDiv').html(response);
                $('#modifyItemModal').modal();
            }
        });
    }
});

$(document).on('click', '#addItemSubmitButton', function () {
    let addItemModal = $('#addItemModal');
    let addItemForm = $('#addAssetForm');
    if (validateModals('addAssetForm')) {
        $.ajax({
            url: 'https://localhost:44388/viewInventory/addItem',
            data: addItemForm.serialize(),
            type: 'POST',
            success: function (response) {
                if (response.success) {
                    alert(response.responseText);
                    addItemModal.modal('toggle');
                    location.reload();
                }
                else {
                    alert(response.responseText);
                }
            }
        });
    }
});


function resetSearchValues() {
    $('#assetTag').val('');
    $('#inventoryOwner').val('');
    $('#locationSelect').val(1);
    $('#clientSiteSelect').val(1);
}



function validateSearchForm() {
    var assetTag = document.forms['takeInventoryForm']['assetTag'];
    var inventoryOwner = document.forms['takeInventoryForm']['inventoryOwner'];

    let fields = [assetTag, inventoryOwner];
    fields.forEach(element => {
        if (element.value == '') {
            element.classList.add('is-invalid');
        }
        else if ((element.type === 'text') && !(isNaN(element.value))) {
            element.classList.add('is-invalid');
        }

        else {
            element.classList.remove('is-invalid');
            element.classList.add('is-valid')
        }
    });

    let isInvalidPreset = fields.some(function (field) {
         return field.classList.contains('is-invalid');
    });

    if (isInvalidPreset) { 
        return false;
    }
    else
        return true;
};

function validateModals(form) {
    var assetTag = document.forms[form]['assetTagModalInput'];
    var serialNumber = document.forms[form]['serialNumberModalInput'];
    var itemName = document.forms[form]['itemNameModalInput'];
    var purchaseDate = document.forms[form]['purchaseDateModalInput'];
    var inventoryOwner = document.forms[form]['inventoryOwnerModalInput'];
    var inventoriedBy = document.forms[form]['inventoriedByModalInput'];
    var inventoryDate = document.forms[form]['inventoriedDateModalInput'];

    let fields = [assetTag, serialNumber, itemName, purchaseDate, inventoryOwner, inventoriedBy, inventoryDate];
    fields.forEach(element => {
        if (element.value == '') {
            element.classList.add('is-invalid');
        }
        else if ((element.type === 'text') && !(isNaN(element.value))) {
            element.classList.add('is-invalid');
        }
        else if (element.type === 'datetime-local' && element.value < '1900-01-01T01:00:00.000'){
            element.classList.add('is-invalid');
        }
        else {
            element.classList.remove('is-invalid');
            element.classList.add('is-valid')
        }
    });

    let isInvalidPreset = fields.some(function (field) {
        return field.classList.contains('is-invalid');
    });

    if (isInvalidPreset) {
        return false;
    }
    else
        return true;
}








