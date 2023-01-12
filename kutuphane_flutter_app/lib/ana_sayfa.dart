import 'dart:io';

import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:kutuphane_flutter_app/Model/member_model.dart';
import 'package:kutuphane_flutter_app/variables.dart';

import 'AnaSayfaPages/main_page.dart';
import 'AnaSayfaPages/mybooks_page.dart';
import 'AnaSayfaPages/popular_page.dart';
import 'AnaSayfaPages/search_page.dart';

class AnaSayfa extends StatefulWidget {
  const AnaSayfa({Key? key}) : super(key: key);

  @override
  _AnaSayfaState createState() => _AnaSayfaState();
}

class _AnaSayfaState extends State<AnaSayfa> {
  int _currentIndex = 0;
  List<Widget> body = const [
    Icon(Icons.home_outlined),
    Icon(Icons.local_fire_department_outlined),
    Icon(Icons.menu_book_outlined),
    Icon(Icons.search_outlined)
  ];

  @override
  void initState() {
    // TODO: implement initState
    if (loginId == 0) {
      Navigator.of(context).pushNamed('/login');
    }
    getMember();
  }

  final screens = [MainPage(), PopularPage(), MyBooksPage(), SearchPage()];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      extendBodyBehindAppBar: true,
      endDrawer: NavigationDrawer(),
      appBar: AppBar(
        automaticallyImplyLeading: false,
        title: const Text('Kütüphane'),
        //actions: <Widget>[IconButton(onPressed: () {}, icon: Icon(Icons.menu))],
        elevation: 0,
        backgroundColor: Colors.transparent,
        bottom: PreferredSize(
          preferredSize: const Size.fromHeight(1),
          child: Container(
            color: Colors.black,
            height: 0.15,
          ),
        ),
      ),
      body: Center(
        child: screens[_currentIndex],
      ),
      bottomNavigationBar: BottomNavigationBar(
        //showUnselectedLabels: false,
        backgroundColor: Color(0xff6dd5ed),
        type: BottomNavigationBarType.fixed,
        selectedItemColor: Colors.white,
        unselectedItemColor: Colors.black,
        currentIndex: _currentIndex,
        onTap: (int newIndex) {
          setState(() {
            _currentIndex = newIndex;
          });
        },
        items: const [
          BottomNavigationBarItem(
            backgroundColor: Colors.cyan,
            label: 'Ana Sayfa',
            icon: Icon(Icons.home_outlined),
          ),
          BottomNavigationBarItem(
            backgroundColor: Colors.cyan,
            label: 'Popüler',
            icon: Icon(Icons.local_fire_department_outlined),
          ),
          BottomNavigationBarItem(
            backgroundColor: Colors.cyan,
            label: 'Kitaplarım',
            icon: Icon(Icons.menu_book_outlined),
          ),
          BottomNavigationBarItem(
            backgroundColor: Colors.cyan,
            label: 'Arama',
            icon: Icon(Icons.search_outlined),
          ),
        ],
      ),
    );
  }
  Future getMember() async{
    var response = await http.get(
      Uri.parse('http://10.0.2.2:44383/api/Member/$loginId'),
    );
    if(response.statusCode==200){
      var jsonresponse = json.decode(response.body);
      for (var i in jsonresponse){
        MemberModel member = MemberModel.fromJson(i);
        AdSoyad = member.AdSoyad;
        Eposta = member.Eposta;
      }
    }
    setState(() {
    });
  }
}

class NavigationDrawer extends StatefulWidget {
  const NavigationDrawer({Key? key}) : super(key: key);

  @override
  _NavigationDrawerState createState() => _NavigationDrawerState();
}

class _NavigationDrawerState extends State<NavigationDrawer> {
  @override
  Widget build(BuildContext context) {
    return Drawer(
      backgroundColor: Color(0xff176C82),
      child: ListView(
        children: <Widget>[
          buildHeader(context),
          Container(
            decoration: const BoxDecoration(
                border: Border(bottom: BorderSide(width: 1, color: Colors.black26))),
            child: ListTile(
              title: Text("Bildirimler",style: TextStyle(color: Colors.white, fontSize: 16),),
              trailing: Icon(Icons.notifications,size: 40,),
              onTap:() =>{Navigator.of(context).pushNamed('/notification')},
            ),
          ),
          //SizedBox(height: MediaQuery.of(context).size.height - 235,),
          Container(
            decoration: const BoxDecoration(
                border: Border(bottom: BorderSide(width: 1, color: Colors.black26))),
            child: ListTile(
              title: Text("Şifre Değiştir",style: TextStyle(color: Colors.white, fontSize: 16),),
              trailing: Icon(Icons.password, size: 40,),
              onTap:() =>{Navigator.of(context).pushNamed('/updatepassword')},
            ),
          ),
        ],
      ),
    );
  }
  Widget buildHeader(BuildContext context) => Container(
    color: Color(0xff104A59),
    padding: EdgeInsets.only(
      top:  MediaQuery.of(context).padding.top,
      bottom: 22,
    ),
    child: Column(
      children: [
        Text(AdSoyad, style: TextStyle(fontSize: 24, color: Colors.white),),
        SizedBox(height: 6,),
        Text(Eposta, style: TextStyle(fontSize: 12, color: Colors.white70),),
      ],
    ),
  );
}
