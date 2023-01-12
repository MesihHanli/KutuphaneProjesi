import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:kutuphane_flutter_app/Model/book_model.dart';
import 'package:http/http.dart' as http;

class SearchPage extends StatefulWidget {
  @override
  _SearchPageState createState() => _SearchPageState();
}

class _SearchPageState extends State<SearchPage> {
  final controller = TextEditingController();
  List<BookModel> books = [];
  String query = "";

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
                  if(book.Durum.contains("silindi")){
                    return SizedBox.shrink();
                  }
                  return buildBook(book);
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
      Uri.parse('http://10.0.2.2:44383/api/Book?searchPhrase=$searchPhrase'),
    );
    if (response.statusCode == 200) {
      var jsonresponse = json.decode(response.body);

      for (var b in jsonresponse) {
        BookModel book = BookModel.fromJson(b);
        books.add(book);
      }
    }
    setState(() {
      query = searchPhrase;
      this.books = books;
    });
  }

  Widget buildBook(BookModel book) => Container(
        decoration: const BoxDecoration(
        border: Border(bottom: BorderSide(width: .5, color: Colors.black26))),
        child: ListTile(
          title: Text(book.Isim,
              style:
                  TextStyle(color: Colors.black87, fontWeight: FontWeight.normal)),
          subtitle: Text(book.Yazar,
              style:
                  TextStyle(color: Colors.black54, fontWeight: FontWeight.w400)),
          trailing: Text("${book.Durum.replaceAll(' ', '').replaceAll('u', 'ü')}de",style: TextStyle(color: Colors.black54),),
        ),
      );
}
