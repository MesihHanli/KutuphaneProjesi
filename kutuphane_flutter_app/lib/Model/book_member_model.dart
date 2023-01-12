import 'package:json_annotation/json_annotation.dart';

part 'book_member_model.g.dart';

@JsonSerializable()
class BookMemberModel{
  final int BookId;
  final String BookName;
  final String Yazar;
  final double OrtalamaPuan;
  final String TeslimTarihi;
  BookMemberModel(this.BookId, this.BookName, this.Yazar, this.OrtalamaPuan, this.TeslimTarihi);

  factory BookMemberModel.fromJson(Map<String, dynamic> json) => _$BookMemberModelFromJson(json);
  Map<String, dynamic> toJson() => _$BookMemberModelToJson(this);
}