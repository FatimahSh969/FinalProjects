
import 'package:flutter/material.dart';
import 'Screens/LoginScreen1.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Online Store',

      theme: ThemeData(
        useMaterial3: true,
        scaffoldBackgroundColor: const Color(0xFFF7F6F2),

        colorScheme: ColorScheme.fromSeed(
          seedColor: const Color(0xFF52796F),
          primary: const Color(0xFF52796F),
          secondary: const Color(0xFFA3B18A),
          surface: const Color(0xFFF7F6F2),
        ),

        appBarTheme: const AppBarTheme(
          backgroundColor: Color(0xFFF7F6F2),
          foregroundColor: Color(0xFF344E41),
          elevation: 0,
          centerTitle: true,
        ),

        elevatedButtonTheme: ElevatedButtonThemeData(
          style: ElevatedButton.styleFrom(
            backgroundColor: const Color(0xFF52796F),
            foregroundColor: Colors.white,
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(12),
            ),
          ),
        ),

        inputDecorationTheme: InputDecorationTheme(
          filled: true,
          fillColor: Colors.white,
          border: OutlineInputBorder(
            borderRadius: BorderRadius.circular(12),
          ),
        ),
      ),

      home: const Loginscreeen1(),
    );
  }
}
