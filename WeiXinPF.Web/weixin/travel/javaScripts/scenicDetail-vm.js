/// <reference path="E:\168TFS\Travel\Travel\SourceCode\Travel.Presentation.Web\javaScripts/knockout-3.3.0.js" />

var ScenicDetailViewModel = function (data) {
    var self = this;
    this.data=ko.observable(data);
    this.processTitle=ko.observable(data.page_title);

};