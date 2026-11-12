
import 'package:flutter/material.dart';
import 'package:foruth_group_mobile1/Services/ProductService.dart';
import 'package:foruth_group_mobile1/Services/CartService.dart';
import 'package:foruth_group_mobile1/Screens/CartScreen.dart';
import 'HomeScreen.dart';

class Productscreen extends StatefulWidget {
  const Productscreen({super.key});

  @override
  State<Productscreen> createState() => _ProductscreenState();
}

class _ProductscreenState extends State<Productscreen> {

  final Productservice productservice = Productservice();

  late Future<List<dynamic>> products;

  @override
  void initState() {
    super.initState();
    products = productservice.getproducts();
  }

  // Reload products if an error happens
  void retryLoading() {
    setState(() {
      products = productservice.getproducts();
    });
  }

  // Add product to cart
  void addProduct(dynamic product) {

    CartService.addToCart(product);

    if (!mounted) return;

    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        content: Text('Product added to cart!'),
        duration: Duration(seconds: 1),
        backgroundColor: Color(0xFF52796F),
      ),
    );
  }

  // Display product details
  void showProductDetails(dynamic product) {

    showDialog(
      context: context,
      builder: (context) {

        return AlertDialog(
          backgroundColor: Color(0xFFF7F6F2),

          title: Text(
            product['title']?.toString() ?? '',
            style: TextStyle(
              color: Color(0xFF344E41),
              fontWeight: FontWeight.bold,
            ),
          ),

          content: SingleChildScrollView(
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [

                Image.network(
                  product['thumbnail']?.toString() ?? '',
                  height: 150,
                  fit: BoxFit.contain,
                  errorBuilder: (context, error, stackTrace) {
                    return Icon(
                      Icons.image_not_supported,
                      size: 50,
                      color: Colors.grey,
                    );
                  },
                ),

                SizedBox(height: 15),

                Text(
                  product['description']?.toString() ??
                      'No description available',
                  textAlign: TextAlign.center,
                ),

                SizedBox(height: 10),

                Text(
                  'Price: \$${product['price'] ?? 0}',
                  style: TextStyle(
                    fontSize: 18,
                    fontWeight: FontWeight.bold,
                    color: Color(0xFF52796F),
                  ),
                ),

              ],
            ),
          ),

          actions: [

            TextButton(
              onPressed: () {
                Navigator.pop(context);
              },
              child: Text(
                'Close',
                style: TextStyle(
                  color: Color(0xFF52796F),
                ),
              ),
            ),

          ],
        );
      },
    );
  }

  // Open cart screen
  void openCart() {
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) => CartScreen(),
      ),
    );
  }

  // Open home screen
  void openHome() {
    Navigator.pushReplacement(
      context,
      MaterialPageRoute(
        builder: (context) => Homescreen(),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {

    return Scaffold(
      backgroundColor: Color(0xFFF7F6F2),

      // AppBar
      appBar: AppBar(
        title: Text(
          'All Products',
          style: TextStyle(
            fontSize: 25,
            fontWeight: FontWeight.bold,
          ),
        ),

        backgroundColor: Color(0xFF52796F),
        foregroundColor: Colors.white,
        centerTitle: true,

        actions: [

          IconButton(
            onPressed: openCart,
            icon: Icon(
              Icons.shopping_cart,
              size: 28,
            ),
          ),

        ],
      ),

      // Body
      body: FutureBuilder<List<dynamic>>(
        future: products,

        builder: (context, snapshot) {

          // Loading
          if (snapshot.connectionState == ConnectionState.waiting) {

            return Center(
              child: CircularProgressIndicator(
                color: Color(0xFF52796F),
              ),
            );
          }

          // Error
          if (snapshot.hasError) {

            return Center(
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: [

                  Icon(
                    Icons.error_outline,
                    size: 50,
                    color: Colors.red,
                  ),

                  SizedBox(height: 10),

                  Text('Something went wrong!'),

                  SizedBox(height: 10),

                  ElevatedButton(
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Color(0xFF52796F),
                      foregroundColor: Colors.white,
                    ),
                    onPressed: retryLoading,
                    child: Text('Retry'),
                  ),

                ],
              ),
            );
          }

          final productList = snapshot.data ?? [];

          // Empty products
          if (productList.isEmpty) {

            return Center(
              child: Text('No Products'),
            );
          }

          // Display products
          return GridView.builder(

            padding: EdgeInsets.all(15),

            gridDelegate:
            SliverGridDelegateWithFixedCrossAxisCount(
              crossAxisCount: 2,
              crossAxisSpacing: 10,
              mainAxisSpacing: 10,
              childAspectRatio: 0.65,
            ),

            itemCount: productList.length,

            itemBuilder: (context, index) {

              final product = productList[index];

              return InkWell(

                // Click on product
                onTap: () {
                  showProductDetails(product);
                },

                child: Card(
                  elevation: 5,
                  color: Colors.white,

                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(15),
                  ),

                  clipBehavior: Clip.antiAlias,

                  child: Column(
                    children: [

                      // Product image
                      Expanded(
                        child: Image.network(
                          product['thumbnail']?.toString() ?? '',
                          width: double.infinity,
                          fit: BoxFit.cover,

                          errorBuilder: (context, error, stackTrace) {
                            return Center(
                              child: Icon(
                                Icons.image_not_supported,
                                size: 50,
                                color: Colors.grey,
                              ),
                            );
                          },
                        ),
                      ),

                      // Product name
                      Padding(
                        padding: EdgeInsets.symmetric(
                          horizontal: 8,
                        ),

                        child: Text(
                          product['title']?.toString() ?? '',
                          maxLines: 2,
                          overflow: TextOverflow.ellipsis,
                          textAlign: TextAlign.center,
                        ),
                      ),

                      SizedBox(height: 8),

                      // Product price
                      Text(
                        'Price: \$${product['price'] ?? 0}',
                        style: TextStyle(
                          fontSize: 15,
                          color: Color(0xFF52796F),
                        ),
                      ),

                      SizedBox(height: 5),

                      // Add to cart button
                      Padding(
                        padding: EdgeInsets.symmetric(
                          horizontal: 8,
                        ),

                        child: SizedBox(
                          width: double.infinity,

                          child: ElevatedButton(
                            style: ElevatedButton.styleFrom(
                              backgroundColor: Color(0xFF52796F),
                              foregroundColor: Colors.white,
                            ),

                            onPressed: () {
                              addProduct(product);
                            },

                            child: Text('Add to Cart'),
                          ),
                        ),
                      ),

                      SizedBox(height: 5),

                    ],
                  ),
                ),
              );
            },
          );
        },
      ),

      // Bottom Navigation Bar
      bottomNavigationBar: BottomNavigationBar(

        backgroundColor: Colors.white,
        selectedItemColor: Color(0xFF52796F),
        unselectedItemColor: Colors.grey,

        currentIndex: 1,

        onTap: (index) {

          if (index == 0) {
            openHome();
          } else if (index == 2) {
            openCart();
          }

        },

        items: [

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
