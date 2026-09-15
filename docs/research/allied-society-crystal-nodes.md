# 友好部族／友好社会クエスト採集地点の属性クリスタル調査

調査日: 2026-09-16  
対象: 新生エオルゼアから黄金のレガシーまで  
目的: 部族クエスト用の密集採集地点に含まれる属性シャード／クリスタル／クラスターのうち、GatherBuddyReborn（GBR）の通常収集から漏れるものを特定する。

## 結論

- ローカルのゲームデータを全拡張横断で照合した結果、部族クエストの採集アイテムと直接紐づく候補は **42 Base**。
- 内訳はナマズオ10、キタリ10、オミクロン10、マムージャ12。採掘系21、園芸系21。
- 42 Baseには合計126 GatheringPointがあるが、既存の `world_locations.json` で座標を使えるのは **41 Base・120 Point**。Base 1195は3 Pointすべて、Base 1198は1 Point、Base 1201は2 Pointの座標がない。
- 先行検証したBase 542を除くと、追加候補は **40 Base・117 Point**。
- 属性はすべてCrystal。Shard／Cluster候補はない。紅蓮以前にも同じ部族クエスト構造の候補はない。
- Base 542はゲーム内で表示とAutoGather到達を確認済み。他の40 Baseはゲームデータ候補であり、実採集・経路は未確認。

## 判定根拠

### ゲームデータで確認済み

1. 各Baseの非CrystalアイテムIDが、`Quest.QuestParams` の `QST_GATHERING_ITEM_MIN` または `QST_GATHERING_ITEM_HRV` に一致する。
2. 一致したQuestの `BeastTribe` は、ナマズオ、キタリ、オミクロン、マムージャのいずれかである。
3. 同じBaseの `GatheringItem -> Item` に属性Crystalが含まれる。
4. 126 Pointの `TerritoryType` はすべて無効値 `1`。このためGBRの通常ノード収集条件から外れる。
5. 120 PointにはGBR同梱の `world_locations.json` にワールド座標がある。

### 公式資料で確認済み

各系統の公式パッチノートは、ギャザラー向けの部族クエストと、受注中だけ採れるクエスト専用素材・採集ポイントの存在を説明している。

- [Patch 4.3 Notes（ナマズオ）](https://na.finalfantasyxiv.com/lodestone/topics/detail/13e322580acb8e9861160a6e08ccabfaff09eeee)
- [Patch 5.2 Notes（キタリ）](https://na.finalfantasyxiv.com/lodestone/topics/detail/b0151eaed1faecb46061b947cf9c08bed75d230d)
- [Patch 6.25 Notes（オミクロン）](https://na.finalfantasyxiv.com/lodestone/topics/detail/2627bf0e00e90852aa6cdc821f337ea9b2c12277)
- [Patch 7.25 Notes（マムージャ）](https://na.finalfantasyxiv.com/lodestone/topics/detail/3c12f110983c5f4288d43aa5ac2ed3c022a75b48)

公式資料は「クエスト未受注時に同じ地点からCrystalだけを採れる」とまでは明記していない。この挙動はBase 542の実機確認から他候補へ外挿した仮説である。

## 候補一覧

`Type`: 0=Mining、1=Quarrying、2=Logging、3=Harvesting。`座標`は `world_locations.json` に存在する数。

| 系統 | Territory | Crystal | Base | Type | GatheringPoint IDs | 座標 |
|---|---:|---|---:|---:|---|---:|
| ナマズオ | 622 The Azim Steppe | 12 Lightning | 541 | 1 | 32314, 32315, 32316 | 3/3 |
| ナマズオ | 622 The Azim Steppe | 12 Lightning | **542（実機確認済み・初回セットから除外）** | 1 | 32317, 32318, 32319 | 3/3 |
| ナマズオ | 622 The Azim Steppe | 12 Lightning | 543 | 1 | 32320, 32321, 32322 | 3/3 |
| ナマズオ | 622 The Azim Steppe | 12 Lightning | 544 | 1 | 32323, 32324, 32325 | 3/3 |
| ナマズオ | 622 The Azim Steppe | 12 Lightning | 545 | 3 | 32326, 32327, 32328 | 3/3 |
| ナマズオ | 622 The Azim Steppe | 12 Lightning | 546 | 3 | 32329, 32330, 32331 | 3/3 |
| ナマズオ | 622 The Azim Steppe | 12 Lightning | 547 | 3 | 32332, 32333, 32334 | 3/3 |
| ナマズオ | 622 The Azim Steppe | 12 Lightning | 548 | 3 | 32335, 32336, 32337 | 3/3 |
| ナマズオ | 622 The Azim Steppe | 12 Lightning | 549 | 1 | 32338, 32339, 32340 | 3/3 |
| ナマズオ | 622 The Azim Steppe | 12 Lightning | 550 | 3 | 32341, 32342, 32343 | 3/3 |
| キタリ | 817 The Rak'tika Greatwood | 10 Wind | 685 | 1 | 32991, 32992, 32993 | 3/3 |
| キタリ | 817 The Rak'tika Greatwood | 10 Wind | 686 | 1 | 32994, 32995, 32996 | 3/3 |
| キタリ | 817 The Rak'tika Greatwood | 10 Wind | 687 | 1 | 32997, 32998, 32999 | 3/3 |
| キタリ | 817 The Rak'tika Greatwood | 10 Wind | 688 | 0 | 33000, 33001, 33002 | 3/3 |
| キタリ | 817 The Rak'tika Greatwood | 10 Wind | 689 | 0 | 33003, 33004, 33005 | 3/3 |
| キタリ | 817 The Rak'tika Greatwood | 10 Wind | 690 | 3 | 33006, 33007, 33008 | 3/3 |
| キタリ | 817 The Rak'tika Greatwood | 10 Wind | 691 | 3 | 33009, 33010, 33011 | 3/3 |
| キタリ | 817 The Rak'tika Greatwood | 10 Wind | 692 | 3 | 33012, 33013, 33014 | 3/3 |
| キタリ | 817 The Rak'tika Greatwood | 10 Wind | 693 | 3 | 33015, 33016, 33017 | 3/3 |
| キタリ | 817 The Rak'tika Greatwood | 10 Wind | 694 | 3 | 33018, 33019, 33020 | 3/3 |
| オミクロン | 960 Ultima Thule | 10 Wind | 905 | 0 | 34364, 34365, 34366 | 3/3 |
| オミクロン | 960 Ultima Thule | 10 Wind | 906 | 1 | 34367, 34368, 34369 | 3/3 |
| オミクロン | 960 Ultima Thule | 13 Water | 907 | 3 | 34370, 34371, 34372 | 3/3 |
| オミクロン | 960 Ultima Thule | 13 Water | 908 | 3 | 34373, 34374, 34375 | 3/3 |
| オミクロン | 960 Ultima Thule | 10 Wind | 909 | 0 | 34376, 34377, 34378 | 3/3 |
| オミクロン | 960 Ultima Thule | 10 Wind | 910 | 0 | 34379, 34380, 34381 | 3/3 |
| オミクロン | 398 The Dravanian Forelands | 8 Fire | 911 | 1 | 34382, 34383, 34384 | 3/3 |
| オミクロン | 960 Ultima Thule | 13 Water | 912 | 3 | 34385, 34386, 34387 | 3/3 |
| オミクロン | 960 Ultima Thule | 13 Water | 913 | 2 | 34388, 34389, 34390 | 3/3 |
| オミクロン | 398 The Dravanian Forelands | 8 Fire | 914 | 3 | 34391, 34392, 34393 | 3/3 |
| マムージャ | 1189 Yak T'el | 9 Ice | 1194 | 0 | 35243, 35244, 35245 | 3/3 |
| マムージャ | 1189 Yak T'el | 9 Ice | 1195 | 1 | 35246, 35247, 35248 | **0/3・定義不可** |
| マムージャ | 1189 Yak T'el | 9 Ice | 1196 | 1 | 35249, 35250, 35251 | 3/3 |
| マムージャ | 1189 Yak T'el | 9 Ice | 1197 | 1 | 35252, 35253, 35254 | 3/3 |
| マムージャ | 1189 Yak T'el | 9 Ice | 1198 | 0 | 35255, **35256**, 35257 | **2/3** |
| マムージャ | 1189 Yak T'el | 9 Ice | 1199 | 0 | 35258, 35259, 35260 | 3/3 |
| マムージャ | 1189 Yak T'el | 13 Water | 1200 | 2 | 35261, 35262, 35263 | 3/3 |
| マムージャ | 1189 Yak T'el | 13 Water | 1201 | 3 | 35264, **35265, 35266** | **1/3** |
| マムージャ | 1189 Yak T'el | 13 Water | 1202 | 2 | 35267, 35268, 35269 | 3/3 |
| マムージャ | 1189 Yak T'el | 13 Water | 1203 | 2 | 35270, 35271, 35272 | 3/3 |
| マムージャ | 1189 Yak T'el | 13 Water | 1204 | 2 | 35273, 35274, 35275 | 3/3 |
| マムージャ | 1189 Yak T'el | 13 Water | 1205 | 3 | 35276, 35277, 35278 | 3/3 |

## 紅蓮以前の確認

- 新生のBase 173はWind Shard／Wind Crystalを含む特殊採集Baseだが、同居アイテム `All-purpose Pigment` は部族クエストの `QST_GATHERING_ITEM_*` と紐づかないため対象外とした。
- 新生・蒼天の部族クエストには、ナマズオ以降と同じ条件で直接紐づく特殊Crystal Baseは検出されなかった。
- したがって「部族クエストのイベント採集地点」という今回の条件では、開始点は紅蓮のナマズオとなる。

## 実装時の扱い

1. Base 1195は利用可能な座標がないため追加しない。
2. Base 1198は35255と35257、Base 1201は35264のみで定義する。座標のないPointを含めると現在のローダーはそのBase全体を拒否する。
3. それ以外は3 Pointすべてを定義する。
4. Base 542以外は候補扱いとし、系統ごとにゲーム内表示・採集・AutoGather到達を確認する。

## 初回実機検証セット

2026-09-16時点では、3地点間の最大3次元距離が最小のBaseだけを各属性から選択した。FireのBase 914と911は、どちらもオミクロンのクエスト用だが、実際の採集Territoryは高地ドラヴァニア。Base 911は実機で採集・到達できたものの、新生エリアの通常採集と比べた優位性が小さいため設定から除外した。Earthは候補なし。

- Lightning: 541
- Wind: 910
- Water: 1205
- Ice: 1197
- Fire: 914（911は実機確認後に除外）

合計5 Base・15 Point。既存のBase 542はLightning候補内で9位だったため置き換えた。

## 未確認事項

- Base 542以外の40 Baseで、クエスト未受注時に該当Crystalが実際に表示・採集できるか。
- 117 PointすべてへのAutoGather到達性。
- 欠損6 Pointのワールド座標。将来 `world_locations.json` に追加された場合はBase 1195の有効化とBase 1198／1201の補完が可能。
- パッチ更新後もBase ID、Point ID、Item IDの対応が維持されるか。

## 使用データ

- インストール済みFFXIVゲームデータ: `F:\Games\SquareEnix\FINAL FANTASY XIV - A Realm Reborn\game\sqpack`
- ゲームバージョン: `2026.09.01.0000.0000`
- 読み取りライブラリ: Dalamud同梱 Lumina 7.0.0.0
- 座標: `GatherBuddy/CustomInfo/world_locations.json`
