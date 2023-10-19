$(function () {
    var l = abp.localization.getResource("DataDictionary");
	
	var dataDictionaryItemService = window.jS.abp.dataDictionary.controllers.dataDictionaryItems.dataDictionaryItem;
	
        var lastNpIdId = '';
        var lastNpDisplayNameId = '';

        var _lookupModal = new abp.ModalManager({
            viewUrl: abp.appPath + "Shared/LookupModal",
            scriptUrl: abp.appPath + "Pages/Shared/lookupModal.js",
            modalClass: "navigationPropertyLookup"
        });

        $('.lookupCleanButton').on('click', '', function () {
            $(this).parent().find('input').val('');
        });

        _lookupModal.onClose(function () {
            var modal = $(_lookupModal.getModal());
            $('#' + lastNpIdId).val(modal.find('#CurrentLookupId').val());
            $('#' + lastNpDisplayNameId).val(modal.find('#CurrentLookupDisplayName').val());
        });
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "DataDictionary/DataDictionaryItems/CreateModal",
        scriptUrl: abp.appPath + "Pages/DataDictionary/DataDictionaryItems/createModal.js",
        modalClass: "dataDictionaryItemCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "DataDictionary/DataDictionaryItems/EditModal",
        scriptUrl: abp.appPath + "Pages/DataDictionary/DataDictionaryItems/editModal.js",
        modalClass: "dataDictionaryItemEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            sequenceMin: $("#SequenceFilterMin").val(),
			sequenceMax: $("#SequenceFilterMax").val(),
			code: $("#CodeFilter").val(),
			displayText: $("#DisplayTextFilter").val(),
			description: $("#DescriptionFilter").val(),
            isActive: (function () {
                var value = $("#IsActiveFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
			dataDictionaryId: $("#DataDictionaryIdFilter").val()
        };
    };
    
    

    var dataTable = $("#DataDictionaryItemsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(dataDictionaryItemService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('DataDictionary.DataDictionaryItems.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.dataDictionaryItem.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('DataDictionary.DataDictionaryItems.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    dataDictionaryItemService.delete(data.record.dataDictionaryItem.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reloadEx();;
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "dataDictionaryItem.sequence" },
			{ data: "dataDictionaryItem.code" },
			{ data: "dataDictionaryItem.displayText" },
			{ data: "dataDictionaryItem.description" },
            {
                data: "dataDictionaryItem.isActive",
                render: function (isActive) {
                    return isActive ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
            {
                data: "dataDictionary.displayText",
                defaultContent : ""
            }
        ]
    }));
    
    

    createModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    editModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    $("#NewDataDictionaryItemButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reloadEx();;
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        dataDictionaryItemService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/data-dictionary/data-dictionary-items/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText },
                            { name: 'sequenceMin', value: input.sequenceMin },
                            { name: 'sequenceMax', value: input.sequenceMax }, 
                            { name: 'code', value: input.code }, 
                            { name: 'displayText', value: input.displayText }, 
                            { name: 'description', value: input.description }, 
                            { name: 'isActive', value: input.isActive }, 
                            { name: 'dataDictionaryId', value: input.dataDictionaryId }
                            ]);
                            
                    var downloadWindow = window.open(url, '_blank');
                    downloadWindow.focus();
            }
        )
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reloadEx();;
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reloadEx();;
    });
    
    
    
    
});
