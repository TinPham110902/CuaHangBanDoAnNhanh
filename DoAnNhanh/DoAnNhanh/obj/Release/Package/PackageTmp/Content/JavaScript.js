function ShowImagePreview(imageUploadder, previewimage) {
    if (imageUploadder.files && imageUploadder.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewimage).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUploadder.files[0])
    }
}

if (document.readyState == 'loading') {
    document.addEventListener('DOMContentLoaded', ready)
} else {
    ready();
}

function ready(){
    //remove item
    var removeCartButtons = document.getElementsByClassName('cart-remove');
    console.log(removeCartButtons);
    for (var i = 0; i < removeCartButtons.length; i++) {
        var button = removeCartButtons[i];
        button.addEventListener("click", removeCartItem);
    }

    // Quantity Changes
    var quantityInputs = document.getElementsByClassName('so-luong');
    for (var i = 0; i < quantityInputs.length; i++) {
        var input = quantityInputs[i];
        input.addEventListener("change", quantityChanged);
    }

    var addCart = document.getElementsByClassName('card-body');
    for (var i = 0; i < addCart.length; i++) {
        var button = addCart[i];
        button.addEventListener("click", addCartClicked);
    }


    document.getElementsByClassName('btn-buy')[0].
        addEventListener("click", buyButtonClicked);
}






function quantityChanged(event) {
    var input = event.target;
    if (isNaN(input.value) || input.value <= 0) {
        input.value = 1;
    }

    updatetotal();
}



function removeCartItem(event) {
    var buttonClicked = event.target;
    buttonClicked.parentElement.remove();
    updatetotal();
}

function addCartClicked(event) {
    var button = event.target;
    var shopProducts = button.parentElement;
    var title = shopProducts.getElementsByClassName('card-title')[0].innerText;
    var price = shopProducts.getElementsByClassName('card-text')[0].innerText;
    addProductToCart(title, price);
    updatetotal();
}

function addProductToCart(title, price) {
    var cartShopBox = document.createElement("div");
    cartShopBox.classList.add('order');
    var cartItems = document.getElementsByClassName("chonmonan")[0];

    var cartItemsNames = cartItems.getElementsByClassName("tenmon");
    for (var i = 0; i < cartItemsNames.length; i++) {
        if (cartItemsNames[i].innerText == title) {
            alert("da co mon an trong gio hang");
            return;
        }
    }
    var cartBoxContent = `
  
                        <div class="stt">1.</div>
                        <div class="tenmon" name="tenmon">${title}</div>
                        <input class="so-luong" type="number" id="quantity" name="quantity" min="1" max="10" value="1">
                        <div class="sotien" name="sotien">${price}</div>
                        <i class='bx bx-trash cart-remove' style="color:red; font-size:25px;"></i>
                   
    `;

    cartShopBox.innerHTML = cartBoxContent;
    cartItems.append(cartShopBox);

    cartShopBox.getElementsByClassName("cart-remove")[0]
        .addEventListener("click", removeCartItem);

    cartShopBox.getElementsByClassName('so-luong')[0].addEventListener("change", quantityChanged);


}



function buyButtonClicked() {
    var cartContent = document.getElementsByClassName('chonmonan')[0]
    while (cartContent.hasChildNodes()) {
        cartContent.removeChild(cartContent.firstChild);
    }
    updatetotal();
}

//update list

function updatetotal() {
    var cartContent = document.getElementsByClassName('chonmonan')[0];
    var cartBoxes = cartContent.getElementsByClassName('order');
    var total = 0;
    for (var i = 0; i < cartBoxes.length; i++) {
        var cartBox = cartBoxes[i];
        var priceElement = cartBox.getElementsByClassName('sotien')[0];
        var quantityElement = cartBox.getElementsByClassName('so-luong')[0];
        var price = parseFloat(priceElement.innerText)*1000;
        var quantity = quantityElement.value;
        total = (total + (price * quantity));

    }
       /* total=Math.round(total *100)/100;*/

    total=new Intl.NumberFormat('en-IN', { maximumSignificantDigits: 3 }).format(total);

        document.getElementsByClassName('tongtien')[0].innerText = total + " " +"vnd"; 
 
}