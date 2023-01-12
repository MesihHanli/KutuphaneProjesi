import 'package:json_annotation/json_annotation.dart';

part 'member_model.g.dart';

@JsonSerializable()
class MemberModel{
  final int Id;
  final String AdSoyad;
  final String Eposta;
  MemberModel(this.Id, this.AdSoyad, this.Eposta);

  factory MemberModel.fromJson(Map<String, dynamic> json) => _$MemberModelFromJson(json);
  Map<String, dynamic> toJson() => _$MemberModelToJson(this);
}