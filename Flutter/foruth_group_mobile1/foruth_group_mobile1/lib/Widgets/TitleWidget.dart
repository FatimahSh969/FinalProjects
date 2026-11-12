
import 'package:flutter/material.dart';

class Titlewidget extends StatelessWidget {
  String TitleName;

  Titlewidget({required this.TitleName});

  @override
  Widget build(BuildContext context) {
    return Expanded(
      flex: 2,
      child: Container(
        padding: EdgeInsets.all(12),
        decoration: BoxDecoration(
          color: Color(0xFFA3B18A),
          borderRadius: BorderRadius.circular(12),
        ),
        child: Text(
          TitleName,
          textAlign: TextAlign.center,
          style: TextStyle(
            fontSize: 20,
            fontWeight: FontWeight.bold,
            color: Color(0xFF344E41),
          ),
        ),
      ),
    );
  }
}
