
import 'package:flutter/material.dart';
import 'package:foruth_group_mobile1/Widgets/GridItemWidget.dart';
import 'package:foruth_group_mobile1/Services/ProductService.dart';
import 'ProductScreen.dart';
import 'CartScreen.dart';

class Homescreen extends StatefulWidget {
  const Homescreen({super.key});

  static const String routeName = 'HomeScreen';

  @override
  State<Homescreen> createState() => _HomescreenState();
}

class _HomescreenState extends State<Homescreen> {

  final Productservice productservice = Productservice();

  late Future<List<dynamic>> products;

  String searchQuery = '';

  @override
  void initState() {
    super.initState();
    products = productservice.getproducts();
  }

  // Display products by category
  Widget buildCategory(
      String title,
      List<dynamic> productList,
      List<String> categories) {

    final categoryProducts = productList.where((product) {
      return categories.contains(product['category']);
    }).take(2).toList();

    if (categoryProducts.isEmpty) {
      return const SizedBox.shrink();
    }

    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [

        Text(
          title,
          style: const TextStyle(
            fontSize: 20,
            fontWeight: FontWeight.bold,
            color: Color(0xFF344E41),
          ),
        ),

        const SizedBox(height: 12),

        Row(
          children: [

            Expanded(
              child: GridItemWidget(
                Imagepath: categoryProducts[0]['thumbnail'],
                ItemName: categoryProducts[0]['title'],
              ),
            ),

            const SizedBox(width: 10),

            if (categoryProducts.length > 1)
              Expanded(
                child: GridItemWidget(
                  Imagepath: categoryProducts[1]['thumbnail'],
                  ItemName: categoryProducts[1]['title'],
                ),
              ),

          ],
        ),

        const SizedBox(height: 25),

      ],
    );
  }

  // Open Products Screen
  void openProducts() {
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) => Productscreen(),
      ),
    );
  }

  // Open Cart Screen
  void openCart() {
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) => CartScreen(),
      ),
    );
  }

  // Reload products from API
  void retryLoading() {
    setState(() {
      products = productservice.getproducts();
    });
  }

  @override
  Widget build(BuildContext context) {

    return Scaffold(
      backgroundColor: const Color(0xFFF7F6F2),

      // Drawer
      drawer: Drawer(
        child: ListView(
          padding: EdgeInsets.zero,
          children: [

            DrawerHeader(
              decoration: const BoxDecoration(
                gradient: LinearGradient(
                  colors: [
                    Color(0xFF84A98C),
                    Color(0xFF52796F),
                  ],
                ),
              ),

              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [

                  const Icon(
                    Icons.shopping_bag,
                    size: 40,
                    color: Colors.white,
                  ),

                  const SizedBox(height: 10),

                  const Text(
                    'Online Store',
                    style: TextStyle(
                      fontSize: 25,
                      fontWeight: FontWeight.bold,
                      color: Colors.white,
                    ),
                  ),

                  const Text(
                    'Your favorite shopping place',
                    style: TextStyle(
                      color: Colors.white,
                      fontSize: 14,
                    ),
                  ),

                ],
              ),
            ),

            ListTile(
              leading: const Icon(
                Icons.home,
                color: Color(0xFF52796F),
              ),
              title: const Text('Home'),
              onTap: () {
                Navigator.pop(context);
              },
            ),

            ListTile(
              leading: const Icon(
                Icons.shopping_bag,
                color: Color(0xFF52796F),
              ),
              title: const Text('Products'),
              onTap: () {
                Navigator.pop(context);
                openProducts();
              },
            ),

            ListTile(
              leading: const Icon(
                Icons.shopping_cart,
                color: Color(0xFF52796F),
              ),
              title: const Text('Cart'),
              onTap: () {
                Navigator.pop(context);
                openCart();
              },
            ),

            const Divider(),

            ListTile(
              leading: const Icon(
                Icons.favorite,
                color: Color(0xFF52796F),
              ),
              title: const Text('Favorites'),
              onTap: () {
                Navigator.pop(context);
              },
            ),

            ListTile(
              leading: const Icon(
                Icons.person,
                color: Color(0xFF52796F),
              ),
              title: const Text('My Account'),
              onTap: () {
                Navigator.pop(context);
              },
            ),

          ],
        ),
      ),

      // AppBar
      appBar: AppBar(
        title: const Text(
          'Online Store',
          style: TextStyle(
            fontWeight: FontWeight.bold,
          ),
        ),
        centerTitle: true,
        backgroundColor: const Color(0xFF52796F),
        foregroundColor: Colors.white,
      ),

      // Body
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(15),

          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [

              // Welcome Container
              Container(
                width: double.infinity,
                padding: const EdgeInsets.all(20),

                decoration: BoxDecoration(
                  borderRadius: BorderRadius.circular(20),

                  gradient: const LinearGradient(
                    colors: [
                      Color(0xFF84A98C),
                      Color(0xFF52796F),
                    ],
                  ),

                  boxShadow: [
                    BoxShadow(
                      color: Colors.grey.withOpacity(0.2),
                      blurRadius: 8,
                      offset: const Offset(2, 4),
                    ),
                  ],
                ),

                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [

                    const Text(
                      'Welcome to Online Store!',
                      style: TextStyle(
                        fontSize: 23,
                        fontWeight: FontWeight.bold,
                        color: Colors.white,
                      ),
                    ),

                    const SizedBox(height: 8),

                    const Text(
                      'Find your favorite products',
                      style: TextStyle(
                        fontSize: 16,
                        color: Colors.white,
                      ),
                    ),

                  ],
                ),
              ),

              const SizedBox(height: 20),

              // Search TextField
              TextField(
                onChanged: (value) {
                  setState(() {
                    searchQuery = value;
                  });
                },

                decoration: InputDecoration(
                  hintText: 'Search products...',

                  prefixIcon: const Icon(
                    Icons.search,
                    color: Color(0xFF52796F),
                  ),

                  filled: true,
                  fillColor: Colors.white,

                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(15),
                    borderSide: BorderSide.none,
                  ),
                ),
              ),

              const SizedBox(height: 25),

              // Get products from API
              FutureBuilder<List<dynamic>>(
                future: products,

                builder: (context, snapshot) {

                  // Loading Indicator
                  if (snapshot.connectionState ==
                      ConnectionState.waiting) {

                    return const Center(
                      child: Padding(
                        padding: EdgeInsets.all(30),
                        child: CircularProgressIndicator(
                          color: Color(0xFF52796F),
                        ),
                      ),
                    );
                  }

                  // Error Message
                  if (snapshot.hasError) {

                    return Center(
                      child: Column(
                        children: [

                          const Icon(
                            Icons.error_outline,
                            size: 50,
                            color: Colors.red,
                          ),

                          const SizedBox(height: 10),

                          const Text(
                            'Error loading products',
                            style: TextStyle(
                              fontSize: 16,
                              color: Color(0xFF344E41),
                            ),
                          ),

                          const SizedBox(height: 10),

                          ElevatedButton(
                            style: ElevatedButton.styleFrom(
                              backgroundColor: const Color(0xFF52796F),
                              foregroundColor: Colors.white,
                            ),

                            onPressed: retryLoading,

                            child: const Text('Retry'),
                          ),

                        ],
                      ),
                    );
                  }

                  final productList = snapshot.data ?? [];

                  // Search products
                  final filteredProducts =
                  productList.where((product) {

                    return product['title']
                        .toString()
                        .toLowerCase()
                        .contains(searchQuery.toLowerCase());

                  }).toList();

                  // No products found
                  if (filteredProducts.isEmpty) {

                    return Center(
                      child: Padding(
                        padding: const EdgeInsets.all(20),

                        child: Text(
                          'No products found',
                          style: const TextStyle(
                            fontSize: 18,
                            color: Color(0xFF52796F),
                          ),
                        ),
                      ),
                    );
                  }

                  // Display Categories
                  return Column(
                    children: [

                      buildCategory(
                        'Beauty',
                        filteredProducts,
                        [
                          'beauty',
                          'fragrances',
                          'skin-care',
                        ],
                      ),

                      buildCategory(
                        'Fashion',
                        filteredProducts,
                        [
                          'tops',
                          'mens-shirts',
                          'mens-shoes',
                          'mens-watches',
                          'womens-bags',
                          'womens-dresses',
                          'womens-jewellery',
                          'womens-shoes',
                          'womens-watches',
                          'sunglasses',
                        ],
                      ),

                      buildCategory(
                        'Electronics',
                        filteredProducts,
                        [
                          'smartphones',
                          'laptops',
                          'tablets',
                          'mobile-accessories',
                        ],
                      ),

                      buildCategory(
                        'Home',
                        filteredProducts,
                        [
                          'furniture',
                          'home-decoration',
                          'kitchen-accessories',
                        ],
                      ),

                      // View All Products Button
                      SizedBox(
                        width: double.infinity,

                        child: ElevatedButton(
                          style: ElevatedButton.styleFrom(
                            backgroundColor: const Color(0xFF52796F),
                            foregroundColor: Colors.white,
                            padding: const EdgeInsets.all(15),
                          ),

                          onPressed: openProducts,

                          child: const Text(
                            'View All Products',
                          ),
                        ),
                      ),

                      const SizedBox(height: 20),

                    ],
                  );
                },
              ),

            ],
          ),
        ),
      ),

      // Floating Action Button
      floatingActionButton: FloatingActionButton(
        tooltip: 'Open Cart',
        backgroundColor: const Color(0xFF52796F),
        foregroundColor: Colors.white,

        onPressed: openCart,

        child: const Icon(Icons.shopping_cart),
      ),

      // Bottom Navigation Bar
      bottomNavigationBar: BottomNavigationBar(
        backgroundColor: Colors.white,
        selectedItemColor: const Color(0xFF52796F),
        unselectedItemColor: Colors.grey,
        currentIndex: 0,

        onTap: (index) {

          if (index == 1) {
            openProducts();
          }

          else if (index == 2) {
            openCart();
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
