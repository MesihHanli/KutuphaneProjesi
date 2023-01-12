import 'package:json_annotation/json_annotation.dart';

part 'password_update_model.g.dart';

@JsonSerializable()
class PasswordUpdateModel{
  final String OldPassword;
  final String NewPassword;
  PasswordUpdateModel(this.OldPassword, this.NewPassword);

  factory PasswordUpdateModel.fromJson(Map<String, dynamic> json) => _$PasswordUpdateModelFromJson(json);
  Map<String, dynamic> toJson() => _$PasswordUpdateModelToJson(this);
}