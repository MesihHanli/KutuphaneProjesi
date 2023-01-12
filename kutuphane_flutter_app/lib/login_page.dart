import 'dart:convert';
import 'dart:io';

import 'package:encrypt/encrypt_io.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:http/http.dart' as http;
import 'package:kutuphane_flutter_app/variables.dart';

import 'main.dart';

import 'package:encrypt/encrypt.dart' as encrypt;
import 'package:pointycastle/asymmetric/api.dart';

class LoginPage extends StatefulWidget {
  const LoginPage({Key? key}) : super(key: key);

  @override
  _LoginPageState createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  @override
  void initState() {
    // TODO: implement initState
    if (loginId != 0) {
      Navigator.of(context).pushNamed('/anasayfa');
    }
  }

  String? email;
  String? password;
  final _formKey = GlobalKey<FormState>();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.blue.shade300,
      resizeToAvoidBottomInset: false,
      body: Form(
        key: _formKey,
        child: Padding(
          padding: const EdgeInsets.only(left: 20, right: 20.0),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              TextFormField(
                autovalidateMode: AutovalidateMode.onUserInteraction,
                decoration: const InputDecoration(
                    focusedBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Colors.white54),
                    ),
                    labelText: "E-posta",
                    labelStyle: TextStyle(
                      color: Colors.white,
                    ),
                    enabledBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Colors.white),
                    ),
                    border: OutlineInputBorder(
                      borderSide: BorderSide(color: Colors.white54),
                    )),
                validator: (value) {
                  if (value == null || value.isEmpty) {
                    return "E-posta Adresinizi Giriniz";
                  } else {
                    return null;
                  }
                },
                onSaved: (value) {
                  email = value;
                },
              ),
              const SizedBox(height: 30.0),
              TextFormField(
                obscureText: true,
                autovalidateMode: AutovalidateMode.onUserInteraction,
                decoration: const InputDecoration(
                    focusedBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Colors.white54),
                    ),
                    labelText: "Şifre",
                    labelStyle: TextStyle(
                      color: Colors.white,
                    ),
                    enabledBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Colors.white),
                    ),
                    border: OutlineInputBorder(
                      borderSide: BorderSide(color: Colors.white54),
                    )),
                validator: (value) {
                  if (value == null || value.isEmpty) {
                    return "Şifrenizi Giriniz";
                  } else {
                    return null;
                  }
                },
                onSaved: (value) {
                  password = value;
                },
              ),
              const SizedBox(
                height: 40.0,
              ),
              _loginButton(),
              const SizedBox(
                height: 10.0,
              ),
              Row(children: <Widget>[
                MaterialButton(
                  child: const Text(
                    "Üye değil misin?\nKaydol",
                    style: TextStyle(color: Colors.white),
                  ),
                  onPressed: () {
                    /*Navigator.pushNamed(
                        context,
                        '/signin'
                    );*/
                    Navigator.of(context).pushNamed('/signup');
                  },
                )
              ]),
            ],
          ),
        ),
      ),
    );
  }

  Future<bool> postData() async {
    final publicPem = await rootBundle.loadString('assets/public-key.pem');
    final publicKey = encrypt.RSAKeyParser().parse(publicPem) as RSAPublicKey;

    /*final privatePem = await rootBundle.loadString('assets/private-key.pem');
     final privateKey = encrypt.RSAKeyParser().parse(privatePem) as RSAPrivateKey;*/

    final encrypter = encrypt.Encrypter(
        encrypt.RSA(publicKey: publicKey /*, privateKey: privateKey*/));
    final encrypted = encrypter.encrypt(password!);

    http.Response response = await http.post(
        Uri.parse('http://10.0.2.2:44383/api/SignInMember'),
        body: {"Eposta": email, "Parola": encrypted.base64});
    //print(response.statusCode);
    if (response.body != "0") {
      try {
        loginId = int.parse(response.body);
      }catch(e){return false;}
      return true;
    } else {
      return false;
    }
  }

  Widget _loginButton() => ElevatedButton(
        onPressed: () async {
          _formKey.currentState?.save();

          if (await postData()) {
            Navigator.of(context).pushNamed('/anasayfa');
          } else {
            print("hata");
          }
        },
        style: TextButton.styleFrom(
          backgroundColor: Colors.white,
          foregroundColor: Colors.blue.shade300,
          minimumSize: Size(350, 50),
          padding: const EdgeInsets.symmetric(horizontal: 16.0),
          shape: const RoundedRectangleBorder(
            borderRadius: BorderRadius.all(Radius.circular(5.0)),
          ),
        ),
        child: const Text("Giriş Yap"),
      );
}
