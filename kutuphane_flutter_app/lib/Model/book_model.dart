import 'package:json_annotation/json_annotation.dart';

part 'book_model.g.dart';

@JsonSerializable()
class BookModel{
  final int Id;
  final String Isim;
  final String Yazar;
  final String Tur;
  final int Sayfa;
  final String Durum;

  BookModel(this.Id, this.Isim, this.Yazar, this.Tur, this.Sayfa, this.Durum);

  factory BookModel.fromJson(Map<String, dynamic> json) => _$BookModelFromJson(json);
  Map<String, dynamic> toJson() => _$BookModelToJson(this);
}
