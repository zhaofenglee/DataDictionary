var abp = abp || {};



abp.modals.dataDictionaryItemEdit = function () {
    var initModal = function (publicApi, args) {
        var l = abp.localization.getResource("DataDictionary");
        
        
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
        
        
        
    };
    
    

    return {
        initModal: initModal
    };
};

