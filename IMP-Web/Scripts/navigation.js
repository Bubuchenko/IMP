//Marks a menu item as selected
var toggleMenuItem = function (itemId) {
    var item = $(itemId.toLowerCase());
    item.toggleClass("selected-item");
    item.find("i").toggleClass("selected-icon");
    item.find("span").toggleClass("selected-text");    
}

