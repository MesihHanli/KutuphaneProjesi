import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:kutuphane_flutter_app/Model/book_model.dart';
import 'package:http/http.dart' as http;
import 'package:kutuphane_flutter_app/variables.dart';
import 'package:flutter_rating_bar/flutter_rating_bar.dart';

class MyBooksPage extends StatefulWidget {
  @override
  _MyBooksPageState createState() => _MyBooksPageState();
}

class _MyBooksPageState extends State<MyBooksPage> {
  final controller = TextEditingController();
  List<BookModel> books = [];
  String query = "";
  List<int> ratings = [];

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    searchBooks(query);
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
            _SearchBox(),
            const Divider(
              color: Colors.white,
              thickness: 1,
              height: 2,
            ),
            Expanded(
              child: ListView.builder(
                padding: const EdgeInsets.all(8),
                itemCount: books.length,
                itemBuilder: (BuildContext context, int index) {
                  final book = books[index];
                  final rating = ratings[index];

                  return buildBook(book, rating);
                },
              ),
            )
          ],
        ),
      ));

  Widget _SearchBox() => Container(
        margin: const EdgeInsets.fromLTRB(16, 86, 16, 10),
        child: TextField(
          controller: controller,
          decoration: InputDecoration(
            prefixIcon: const Icon(Icons.search),
            hintText: 'Kitap Adı',
            border: OutlineInputBorder(
                borderRadius: BorderRadius.circular(20),
                borderSide: const BorderSide(color: Colors.yellow) //Sarı?
                ),
          ),
          onSubmitted: searchBooks,
          //onChanged: filterBooks,
        ),
      );

  Future searchBooks(String searchPhrase) async {
    List<BookModel> books = [];
    var response = await http.get(
      Uri.parse(
          'http://10.0.2.2:44383/api/MembersBook?id=$loginId &searchPhrase=$searchPhrase'),
    );
    if (response.statusCode == 200) {
      var jsonresponse = json.decode(response.body);

      for (var b in jsonresponse) {
        BookModel book = BookModel.fromJson(b);
        books.add(book);
      }
    }
    var responsePuan = await http.get(
      Uri.parse('http://10.0.2.2:44383/api/MembersPuan?id=$loginId'),
    );
    if (responsePuan.statusCode == 200) {
      var jsonresponsePuan = json.decode(responsePuan.body);

      for (var p in jsonresponsePuan) {
        int puan = p;
        ratings.add(puan);
      }
    }

    setState(() {
      query = searchPhrase;
      this.books = books;
    });
  }

  Widget buildBook(BookModel book, int rating) => ListTile(
        title: Text(book.Isim),
        subtitle: Text(book.Yazar),
        trailing: RatingBar.builder(
          itemSize: 30,
          initialRating: rating.toDouble(),
          //allowHalfRating: true,
          minRating: 1,
          itemBuilder: (context, _) => const Icon(
            Icons.star,
            color: Colors.white,
          ),
          onRatingUpdate: (r) async {
            http.Response response = await http.post(
              Uri.parse('http://10.0.2.2:44383/api/MembersPuan'),
              body: {"BookId": book.Id.toString(), "MemberId": loginId.toString(), "Puan": r.toInt().toString()},
            );
          },
        ),
      );
}
