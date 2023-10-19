$(function () {
    var l = abp.localization.getResource("DataDictionary");
	
	var dataDictionaryService = window.jS.abp.dataDictionary.controllers.dataDictionaries.dataDictionary;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "DataDictionary/DataDictionaries/CreateModal",
        scriptUrl: abp.appPath + "Pages/DataDictionary/DataDictionaries/createModal.js",
        modalClass: "dataDictionaryCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "DataDictionary/DataDictionaries/EditModal",
        scriptUrl: abp.appPath + "Pages/DataDictionary/DataDictionaries/editModal.js",
        modalClass: "dataDictionaryEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            code: $("#CodeFilter").val(),
			displayText: $("#DisplayTextFilter").val(),
			description: $("#DescriptionFilter").val(),
            isActive: (function () {
                var value = $("#IsActiveFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })()
        };
    };
    
    

    var dataTable = $("#DataDictionariesTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(dataDictionaryService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("EditDetails"),
                                visible: abp.auth.isGranted('DataDictionary.DataDictionaries'),
                                action: function(data) {
                                    window.location = "/DataDictionary/DataDictionaryItems/" + data.record.id;
                                }
                            },
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('DataDictionary.DataDictionaries.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('DataDictionary.DataDictionaries.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    dataDictionaryService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reloadEx();;
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "code" },
			{ data: "displayText" },
			{ data: "description" },
            {
                data: "isActive",
                render: function (isActive) {
                    return isActive ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            }
        ]
    }));
    
    

    createModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    editModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    $("#NewDataDictionaryButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reloadEx();;
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        dataDictionaryService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/data-dictionary/data-dictionaries/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'code', value: input.code }, 
                            { name: 'displayText', value: input.displayText }, 
                            { name: 'description', value: input.description }, 
                            { name: 'isActive', value: input.isActive }
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
