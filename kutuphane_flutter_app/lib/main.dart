import 'dart:io';
import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:kutuphane_flutter_app/login_page.dart';
import 'package:kutuphane_flutter_app/notification_page.dart';
import 'package:kutuphane_flutter_app/signup_page.dart';
import 'package:kutuphane_flutter_app/ana_sayfa.dart';
import 'package:kutuphane_flutter_app/uppdate_password_page.dart';


void main() async {
  WidgetsFlutterBinding.ensureInitialized();

  ByteData data = await PlatformAssetBundle().load('assets/ca/lets-encrypt-r3.pem');
  SecurityContext.defaultContext.setTrustedCertificatesBytes(data.buffer.asUint8List());
  HttpOverrides.global = MyHttpOverrides();


  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      home: LoginPage(),
      theme: ThemeData(
        //primarySwatch: Colors.grey,
        scaffoldBackgroundColor: Colors.cyan
      ),
      routes: {
        '/signup':(_) => SignUpPage(),
        '/anasayfa':(_) => AnaSayfa(),
        '/login':(_) => LoginPage(),
        '/notification':(_) => NotificationPage(),
        '/updatepassword':(_) => UpdatePasswordPage()
      },
    );
  }
}

class MyHttpOverrides extends HttpOverrides{
  @override
  HttpClient createHttpClient(SecurityContext? context){
    return super.createHttpClient(context)
      ..badCertificateCallback = (X509Certificate cert, String host, int port)=> true;
  }
}