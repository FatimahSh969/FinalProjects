
import 'package:flutter/material.dart';
import 'package:foruth_group_mobile1/Services/CartService.dart';
import 'HomeScreen.dart';
import 'ProductScreen.dart';

class CartScreen extends StatefulWidget {
  const CartScreen({super.key});

  @override
  State<CartScreen> createState() => _CartScreenState();
}

class _CartScreenState extends State<CartScreen> {

  List<Map<String, dynamic>> cartItems = CartService.cartItems;

  void incrementCounter(int index) {
    setState(() {
      cartItems[index]['quantity']++;
    });
  }

  void decrementCounter(int index) {
    setState(() {
      if (cartItems[index]['quantity'] > 1) {
        cartItems[index]['quantity']--;
      }
    });
  }

  void removeItem(int index) {
    setState(() {
      cartItems.removeAt(index);
    });

    ScaffoldMessenger.of(context).showSnackBar(
      const SnackBar(
        content: Text('Product removed from cart'),
        duration: Duration(seconds: 1),
      ),
    );
  }

  void confirmRemove(int index) {
    showDialog(
      context: context,
      builder: (context) {
        return AlertDialog(
          title: const Row(
            children: [
              Icon(
                Icons.warning,
                color: Colors.red,
              ),
              SizedBox(width: 8),
              Text('Remove Product'),
            ],
          ),

          content: const Text(
            'Are you sure you want to remove this product from your cart?',
          ),

          actions: [
            TextButton(
              onPressed: () {
                Navigator.pop(context);
              },
              child: const Text(
                'Cancel',
                style: TextStyle(
                  color: Colors.grey,
                ),
              ),
            ),

            TextButton(
              onPressed: () {
                Navigator.pop(context);
                removeItem(index);
              },
              child: const Text(
                'Remove',
                style: TextStyle(
                  color: Colors.red,
                  fontWeight: FontWeight.bold,
                ),
              ),
            ),
          ],
        );
      },
    );
  }

  double calculateTotal() {
    double total = 0;

    for (var item in cartItems) {
      total += ((item['price'] as num) *
          (item['quantity'] as num)).toDouble();
    }

    return total;
  }

  void confirmCheckout() {

    if (cartItems.isEmpty) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('Your cart is empty'),
        ),
      );
      return;
    }

    showDialog(
      context: context,
      builder: (context) {
        return AlertDialog(
          title: const Text(
            'Confirm Order',
            style: TextStyle(
              color: Color(0xFF344E41),
              fontWeight: FontWeight.bold,
            ),
          ),

          content: Text(
            'Are you sure you want to place your order?\n'
                'Total: \$${calculateTotal().toStringAsFixed(2)}',
          ),

          actions: [
            TextButton(
              onPressed: () {
                Navigator.pop(context);
              },
              child: const Text(
                'Cancel',
                style: TextStyle(color: Colors.grey),
              ),
            ),

            TextButton(
              onPressed: () {
                Navigator.pop(context);

                ScaffoldMessenger.of(context).showSnackBar(
                  const SnackBar(
                    content: Text('Order placed successfully!'),
                  ),
                );
              },
              child: const Text(
                'Confirm',
                style: TextStyle(
                  color: Color(0xFF52796F),
                  fontWeight: FontWeight.bold,
                ),
              ),
            ),
          ],
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {

    return Scaffold(
      backgroundColor: const Color(0xFFF7F6F2),

      appBar: AppBar(
        title: const Text(
          'My Cart',
          style: TextStyle(
            color: Colors.white,
            fontWeight: FontWeight.bold,
            fontSize: 25,
          ),
        ),
        backgroundColor: const Color(0xFF52796F),
        foregroundColor: Colors.white,
        centerTitle: true,
      ),

      body: Column(
        children: [

          Expanded(
            child: cartItems.isEmpty

                ? Center(
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [

                  const Icon(
                    Icons.shopping_cart_outlined,
                    size: 75,
                    color: Color(0xFF84A98C),
                  ),

                  const SizedBox(height: 15),

                  const Text(
                    'Your cart is empty',
                    style: TextStyle(
                      fontSize: 20,
                      color: Color(0xFF52796F),
                      fontWeight: FontWeight.bold,
                    ),
                  ),

                  const SizedBox(height: 15),

                  OutlinedButton(
                    style: OutlinedButton.styleFrom(
                      foregroundColor: const Color(0xFF52796F),
                      side: const BorderSide(
                        color: Color(0xFF52796F),
                      ),
                    ),

                    onPressed: () {
                      Navigator.pushReplacement(
                        context,
                        MaterialPageRoute(
                          builder: (context) => const Productscreen(),
                        ),
                      );
                    },

                    child: const Text('Continue Shopping'),
                  ),

                ],
              ),
            )

                : ListView.builder(
              padding: const EdgeInsets.all(15),
              itemCount: cartItems.length,

              itemBuilder: (context, index) {

                return Container(
                  margin: const EdgeInsets.only(bottom: 15),
                  padding: const EdgeInsets.all(12),

                  decoration: BoxDecoration(
                    color: Colors.white,
                    borderRadius: BorderRadius.circular(15),
                    boxShadow: [
                      BoxShadow(
                        color: Colors.grey.withOpacity(0.15),
                        blurRadius: 6,
                        offset: const Offset(2, 3),
                      ),
                    ],
                  ),

                  child: Row(
                    children: [

                      Image.network(
                        cartItems[index]['image']?.toString() ?? '',
                        width: 85,
                        height: 85,
                        fit: BoxFit.cover,

                        errorBuilder: (context, error, stackTrace) {
                          return const SizedBox(
                            width: 85,
                            height: 85,
                            child: Icon(
                              Icons.image_not_supported,
                              size: 35,
                              color: Colors.grey,
                            ),
                          );
                        },
                      ),

                      const SizedBox(width: 12),

                      Expanded(
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,

                          children: [

                            Text(
                              cartItems[index]['name']?.toString() ?? '',
                              maxLines: 2,
                              overflow: TextOverflow.ellipsis,

                              style: const TextStyle(
                                fontSize: 16,
                                fontWeight: FontWeight.bold,
                                color: Color(0xFF344E41),
                              ),
                            ),

                            const SizedBox(height: 8),

                            Text(
                              '\$${cartItems[index]['price']}',
                              style: const TextStyle(
                                fontSize: 16,
                                color: Color(0xFF52796F),
                              ),
                            ),

                            const SizedBox(height: 8),

                            Row(
                              children: [

                                IconButton(
                                  onPressed: () {
                                    decrementCounter(index);
                                  },

                                  icon: const Icon(
                                    Icons.remove_circle,
                                    color: Color(0xFF52796F),
                                  ),
                                ),

                                Text(
                                  '${cartItems[index]['quantity']}',

                                  style: const TextStyle(
                                    fontSize: 18,
                                    fontWeight: FontWeight.bold,
                                  ),
                                ),

                                IconButton(
                                  onPressed: () {
                                    incrementCounter(index);
                                  },

                                  icon: const Icon(
                                    Icons.add_circle,
                                    color: Color(0xFF52796F),
                                  ),
                                ),

                              ],
                            ),

                          ],
                        ),
                      ),

                      IconButton(
                        onPressed: () {
                          confirmRemove(index);
                        },

                        icon: const Icon(
                          Icons.delete_outline,
                          color: Colors.red,
                        ),
                      ),

                    ],
                  ),
                );
              },
            ),
          ),

          // Total and Buttons
          Container(
            padding: const EdgeInsets.all(20),

            decoration: BoxDecoration(
              color: Colors.white,
              borderRadius: const BorderRadius.only(
                topLeft: Radius.circular(20),
                topRight: Radius.circular(20),
              ),

              boxShadow: [
                BoxShadow(
                  color: Colors.grey,
                  blurRadius: 8,
                  offset: const Offset(0, -3),
                ),
              ],
            ),

            child: Column(
              children: [

                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,

                  children: [

                    const Text(
                      'Total:',
                      style: TextStyle(
                        fontSize: 20,
                        fontWeight: FontWeight.bold,
                        color: Color(0xFF344E41),
                      ),
                    ),

                    Text(
                      '\$${calculateTotal().toStringAsFixed(2)}',

                      style: const TextStyle(
                        fontSize: 22,
                        fontWeight: FontWeight.bold,
                        color: Color(0xFF52796F),
                      ),
                    ),

                  ],
                ),

                const SizedBox(height: 15),

                SizedBox(
                  width: double.infinity,

                  child: OutlinedButton(
                    style: OutlinedButton.styleFrom(
                      foregroundColor: const Color(0xFF52796F),
                      padding: const EdgeInsets.all(13),
                      side: const BorderSide(
                        color: Color(0xFF52796F),
                      ),
                    ),

                    onPressed: () {
                      Navigator.pushReplacement(
                        context,
                        MaterialPageRoute(
                          builder: (context) => const Productscreen(),
                        ),
                      );
                    },

                    child: const Text('Continue Shopping'),
                  ),
                ),

                const SizedBox(height: 10),

                SizedBox(
                  width: double.infinity,

                  child: ElevatedButton(
                    style: ElevatedButton.styleFrom(
                      backgroundColor: const Color(0xFF52796F),
                      foregroundColor: Colors.white,
                      padding: const EdgeInsets.all(15),

                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(12),
                      ),
                    ),

                    onPressed: confirmCheckout,

                    child: const Text(
                      'Checkout',
                      style: TextStyle(
                        fontSize: 18,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ),
                ),

              ],
            ),
          ),

        ],
      ),

      bottomNavigationBar: BottomNavigationBar(

        backgroundColor: Colors.white,
        selectedItemColor: const Color(0xFF52796F),
        unselectedItemColor: Colors.grey,

        currentIndex: 2,

        onTap: (index) {

          if (index == 0) {

            Navigator.pushReplacement(
              context,
              MaterialPageRoute(
                builder: (context) => Homescreen(),
              ),
            );

          } else if (index == 1) {

            Navigator.pushReplacement(
              context,
              MaterialPageRoute(
                builder: (context) => const Productscreen(),
              ),
            );

          }

        },

        items: const [

          BottomNavigationBarItem(
            icon: Icon(Icons.home),
            label: 'Home',
          ),

          BottomNavigationBarItem(
            icon: Icon(Icons.shopping_bag),
            label: 'Products',
          ),

          BottomNavigationBarItem(
            icon: Icon(Icons.shopping_cart),
            label: 'Cart',
          ),

        ],
      ),

    );
  }
}
