import 'package:flutter/material.dart';
import 'package:kutuphane_flutter_app/Model/book_member_model.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'package:kutuphane_flutter_app/variables.dart';
import 'package:flutter_rating_bar/flutter_rating_bar.dart';

class PopularPage extends StatefulWidget {
  @override
  _PopularPageState createState() => _PopularPageState();
}

class _PopularPageState extends State<PopularPage> {
  List<BookMemberModel> popularBooks = [];

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    getPopularBooks();
  }

  @override
  Widget build(BuildContext context) => Scaffold(
        body: Container(
          decoration: const BoxDecoration(
              gradient: LinearGradient(
            colors: [Color(0xff2193b0), Color(0xff6dd5ed)],
            begin: Alignment.topCenter,
            end: Alignment.bottomCenter,
          )),
          child: Column(
            children: <Widget>[
              Container(
                margin: const EdgeInsets.fromLTRB(0, 86, 0, 10),
                decoration: const BoxDecoration(
                  color: Color(0xff176C82),
                    border: Border(
                        bottom: BorderSide(width: 3, color: Colors.black26))),
                child: Column(
                  children: [
                    Container(width: double.infinity, height: 20),
                    Text("Popüler Kitaplar",
                        style: TextStyle(fontSize: 18, color: Colors.white)),
                    Container(width: double.infinity, height: 15,)
                  ],
                ),
              ),
              // _SearchBox(),
              //
              // const Divider(
              //   color: Colors.white,
              //   thickness: 1,
              //   height: 2,
              // ),
              Expanded(
                child: ListView.builder(
                  padding: const EdgeInsets.all(8),
                  itemCount: popularBooks.length,
                  itemBuilder: (BuildContext context, int index) {
                    final book = popularBooks[index];

                    return buildBook(book);
                  },
                ),
              )
            ],
          ),
        ),
      );

  Future getPopularBooks() async {
    List<BookMemberModel> popularBooks = [];
    var response = await http.get(
      Uri.parse('http://10.0.2.2:44383/api/MembersPuan'),
    );
    if (response.statusCode == 200) {
      var jsonresponse = json.decode(response.body);

      for (var b in jsonresponse) {
        BookMemberModel bm = BookMemberModel.fromJson(b);
        popularBooks.add(bm);
      }
    }
    setState(() {
      this.popularBooks = popularBooks;
    });
  }

  Widget buildBook(BookMemberModel bookMember) => ListTile(
        title: Text(bookMember.BookName),
        subtitle: Text(bookMember.Yazar),
        trailing: RatingBarIndicator(
          itemSize: 30,
          rating: bookMember.OrtalamaPuan,
          //allowHalfRating: true,
          //minRating: 1,
          itemBuilder: (context, _) => const Icon(
            Icons.star,
            color: Colors.white,
          ),
          //onRatingUpdate: (r)  {},
        ),
      );
}
