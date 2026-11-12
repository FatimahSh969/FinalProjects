import 'dart:convert';

import 'package:foruth_group_mobile1/data/connection.dart';
import 'package:http/http.dart' as http;

class Productservice {
  Future<List<dynamic>> getproducts() async {
    final url = Uri.parse(ConnectionString.Prodects);
    final response = await http.get(url);

    if (response.statusCode == 200) {
      final Map<String, dynamic> data = jsonDecode(response.body);
      final List<dynamic> products = data['products'] as List<dynamic>;
      return products;
    } else {
      throw Exception('Failed to load Data ${response.statusCode}');
    }
  }
}
