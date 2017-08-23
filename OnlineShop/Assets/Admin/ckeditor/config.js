/*
Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function( config )
{
	// Define changes to default configuration here. For example:
	config.language = 'en';
	// config.uiColor = '#AADC6E';
    // Cấu hình đường dẫn các loại tệp tin khi finder
    config.filebrowserBrowseUrl = "/Assets/Admin/ckfinder/ckfinder.html";
    config.filebrowserImageUrl = "/Assets/Admin/ckfinder/ckfinder.html?type=Images";
    config.filebrowserFlashUrl = "/Assets/Admin/ckfinder/ckfinder.html?type=Flash";
    config.filebrowserUploadUrl = "/Assets/Admin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files";
    config.filebrowserImageUploadUrl = "/Assets/Admin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images";
    config.filebrowserFlashUploadUrl = "/Assets/Admin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash";
};
