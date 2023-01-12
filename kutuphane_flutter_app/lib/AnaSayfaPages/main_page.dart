import 'package:flutter/material.dart';
import 'package:kutuphane_flutter_app/Model/book_member_model.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'package:kutuphane_flutter_app/variables.dart';
import 'package:flutter_rating_bar/flutter_rating_bar.dart';

class MainPage extends StatefulWidget {
  @override
  _MainPageState createState() => _MainPageState();
}

class _MainPageState extends State<MainPage> {
  List<BookMemberModel> myBooks = [];

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    getMyBooks();
  }

  @override
  Widget build(BuildContext context) => Scaffold(
    body: Container(
      decoration: const BoxDecoration(
          gradient: LinearGradient(
            colors: [
              Color(0xff2193b0),
              Color(0xff6dd5ed)
            ],
            begin: Alignment.topCenter,
            end: Alignment.bottomCenter,
          )
      ),
      child: Column(
        children: <Widget>[
          Container(
            margin: const EdgeInsets.fromLTRB(0, 86, 0, 10),
            decoration: const BoxDecoration(
              color: Color(0xff176C82),
                border: Border(bottom: BorderSide(width: 3, color: Colors.black26))),
            child: ListTile(
              
              title: Transform.translate(offset: Offset(36, 0),child: Text("Kitap",style: TextStyle(color: Colors.white)),),
              trailing:Transform.translate(offset: Offset(-12, 0),child: Text("Teslim Tarihi",style: TextStyle(color: Colors.white)),),
            ),
          ),
          Expanded(
            child: ListView.builder(
              padding: const EdgeInsets.all(8),
              itemCount: myBooks.length,

              itemBuilder: (BuildContext context, int index) {
                final book = myBooks[index];

                return buildBook(book);
              },),
          )
        ],
      ),
    ),
  );

  Future getMyBooks() async {
    List<BookMemberModel> popularBooks = [];
    var response = await http.get(
      Uri.parse('http://10.0.2.2:44383/api/MembersActiveBooks/$loginId'),
    );
    if (response.statusCode == 200) {
      var jsonresponse = json.decode(response.body);

      for (var b in jsonresponse) {
        BookMemberModel bm = BookMemberModel.fromJson(b);
        popularBooks.add(bm);
      }
    }
    setState(() {
      this.myBooks = popularBooks;
    });
  }

  Widget buildBook(BookMemberModel bookMember) => Container(

    child: ListTile(
    title: Text("${bookMember.BookName} - id: ${bookMember.BookId.toString()}"),
    subtitle: Text(bookMember.Yazar),
    trailing: Text(bookMember.TeslimTarihi),
    ),
    decoration: const BoxDecoration(
        border: Border(bottom: BorderSide(width: 1, color: Colors.black26))),
  );
}
