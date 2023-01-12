import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:kutuphane_flutter_app/Model/notification_model.dart';
import 'dart:convert';
import 'package:kutuphane_flutter_app/variables.dart';
import 'package:encrypt/encrypt.dart' as encrypt;
import 'package:pointycastle/asymmetric/api.dart';
import 'package:flutter/services.dart';

class UpdatePasswordPage extends StatefulWidget {
  const UpdatePasswordPage({Key? key}) : super(key: key);

  @override
  _UpdatePasswordPageState createState() => _UpdatePasswordPageState();
}

class _UpdatePasswordPageState extends State<UpdatePasswordPage> {
  final formKey = GlobalKey<FormState>();
  String updateControl = "";
  String oldPassword = "";
  String newPassword = "";

  @override
  Widget build(BuildContext context) => Scaffold(
        appBar: AppBar(
          backgroundColor: Color(0xff176C82),
          title: Text("Şifrenizi Değiştirin"),
        ),
        body: Form(
          key: formKey,
          child: ListView(
            padding: EdgeInsets.all(16),
            children: [
              Text(updateControl),
              const SizedBox(height: 15,),
              TextFormField(
                decoration: const InputDecoration(
                  labelText: 'Eski Şifre',
                  border: OutlineInputBorder(),
                ),
                validator: ((value) {
                  if ((value?.length)! < 6) {
                    return 'Şifre 6 karakterden kısa olamaz';
                  } else {
                    return null;
                  }
                }),
                onSaved: (value) => setState(() {
                  oldPassword =   value!;
                }),
                obscureText: true,
              ),
              const SizedBox(
                height: 16,
              ),
              TextFormField(
                decoration: const InputDecoration(
                  labelText: 'Yeni Şifre',
                  border: OutlineInputBorder(),
                ),
                validator: ((value) {
                if ((value?.length)! < 6) {
                return 'Şifre 6 karakter ya da daha uzun olamalı';
                } else {
                  return null;
                }
                }),
                onSaved: (value) => setState(() {
                  newPassword = value!;
                }),
                obscureText: true,
              ),
              const SizedBox(
                height: 32,
              ),
              ElevatedButton(
                onPressed: (){
                  final isValid = formKey.currentState?.validate();
                  if (isValid!){
                    formKey.currentState?.save();
                    updatePassword();
                  }
                },
                child: const Text("Şifre Değiştir"),
              )
            ],
          ),
        ),
      );

  Future updatePassword() async {
    String isUpdated = "";
    String oldp = await EncryptPassword(oldPassword);
    String newp = await EncryptPassword(newPassword);
    var response = await http.put(
        Uri.parse('http://10.0.2.2:44383/api/Member?Eposta=${Eposta}'),
        body: {
          "OldPassword": oldp,
          "NewPassword": newp
        });
    if (response.statusCode == 200) {
      if (response.body == "true") {
        isUpdated = "Şifre Yenileme Başarılı";
      } else {
        isUpdated = "Şifre Yenileme Başarısız!";
      }
    } else {
      isUpdated = "Şifre Yenileme Başarısız!";
    }
    setState(() {
      updateControl = isUpdated;
    });
  }

  Future<String> EncryptPassword(String password) async {
    final publicPem = await rootBundle.loadString('assets/public-key.pem');
    final publicKey = encrypt.RSAKeyParser().parse(publicPem) as RSAPublicKey;

    /*final privatePem = await rootBundle.loadString('assets/private-key.pem');
     final privateKey = encrypt.RSAKeyParser().parse(privatePem) as RSAPrivateKey;*/

    final encrypter = encrypt.Encrypter(
        encrypt.RSA(publicKey: publicKey /*, privateKey: privateKey*/));
    final encrypted = encrypter.encrypt(password);

    return encrypted.base64;
  }
}
