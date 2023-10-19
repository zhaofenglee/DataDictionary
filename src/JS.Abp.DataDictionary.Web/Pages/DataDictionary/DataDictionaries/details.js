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







});
