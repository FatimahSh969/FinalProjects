
class CartService {

  static List<Map<String, dynamic>> cartItems = [];

  static void addToCart(Map<String, dynamic> product) {

    int index = cartItems.indexWhere(
            (item) => item['id'] == product['id']
    );

    if (index != -1) {
      cartItems[index]['quantity']++;
    } else {
      cartItems.add({
        'id': product['id'],
        'name': product['title'],
        'price': product['price'],
        'image': product['thumbnail'],
        'quantity': 1,
      });
    }
  }
}
