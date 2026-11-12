
import 'package:flutter/material.dart';

class GridItemWidget extends StatelessWidget {

  final String Imagepath;
  final String ItemName;

  const GridItemWidget({
    super.key,
    required this.Imagepath,
    required this.ItemName,
  });

  @override
  Widget build(BuildContext context) {

    return Container(
      height: 180,

      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(15),

        boxShadow: [
          BoxShadow(
            color: Colors.grey.withOpacity(0.2),
            blurRadius: 5,
            offset: Offset(2, 3),
          ),
        ],
      ),

      child: ClipRRect(
        borderRadius: BorderRadius.circular(15),

        child: Stack(
          fit: StackFit.expand,

          children: [

            // Product Image
            Image.network(
              Imagepath,
              fit: BoxFit.cover,

              errorBuilder: (context, error, stackTrace) {
                return Center(
                  child: Icon(
                    Icons.image_not_supported,
                    size: 35,
                    color: Colors.grey,
                  ),
                );
              },
            ),

            // Product Name
            Align(
              alignment: Alignment.bottomCenter,

              child: Container(
                width: double.infinity,
                padding: EdgeInsets.all(10),
                color: Color(0x99000000),

                child: Text(
                  ItemName,
                  maxLines: 2,
                  overflow: TextOverflow.ellipsis,
                  textAlign: TextAlign.center,

                  style: TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.bold,
                    color: Colors.white,
                  ),
                ),
              ),
            ),

          ],
        ),
      ),
    );
  }
}
