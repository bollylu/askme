<map version="freeplane 1.5.9">
<!--To view this file, download free mind mapping software Freeplane from http://freeplane.sourceforge.net -->
<node TEXT="AskMe" FOLDED="false" ID="ID_1223514539" CREATED="1471355120753" MODIFIED="1471355137748" STYLE="oval">
<font SIZE="18"/>
<hook NAME="MapStyle">
    <properties fit_to_viewport="false;"/>

<map_styles>
<stylenode LOCALIZED_TEXT="styles.root_node" STYLE="oval" UNIFORM_SHAPE="true" VGAP_QUANTITY="24.0 pt">
<font SIZE="24"/>
<stylenode LOCALIZED_TEXT="styles.predefined" POSITION="right" STYLE="bubble">
<stylenode LOCALIZED_TEXT="default" COLOR="#000000" STYLE="fork">
<font NAME="SansSerif" SIZE="10" BOLD="false" ITALIC="false"/>
</stylenode>
<stylenode LOCALIZED_TEXT="defaultstyle.details"/>
<stylenode LOCALIZED_TEXT="defaultstyle.attributes">
<font SIZE="9"/>
</stylenode>
<stylenode LOCALIZED_TEXT="defaultstyle.note" COLOR="#000000" BACKGROUND_COLOR="#ffffff" TEXT_ALIGN="LEFT"/>
<stylenode LOCALIZED_TEXT="defaultstyle.floating">
<edge STYLE="hide_edge"/>
<cloud COLOR="#f0f0f0" SHAPE="ROUND_RECT"/>
</stylenode>
</stylenode>
<stylenode LOCALIZED_TEXT="styles.user-defined" POSITION="right" STYLE="bubble">
<stylenode LOCALIZED_TEXT="styles.topic" COLOR="#18898b" STYLE="fork">
<font NAME="Liberation Sans" SIZE="10" BOLD="true"/>
</stylenode>
<stylenode LOCALIZED_TEXT="styles.subtopic" COLOR="#cc3300" STYLE="fork">
<font NAME="Liberation Sans" SIZE="10" BOLD="true"/>
</stylenode>
<stylenode LOCALIZED_TEXT="styles.subsubtopic" COLOR="#669900">
<font NAME="Liberation Sans" SIZE="10" BOLD="true"/>
</stylenode>
<stylenode LOCALIZED_TEXT="styles.important">
<icon BUILTIN="yes"/>
</stylenode>
</stylenode>
<stylenode LOCALIZED_TEXT="styles.AutomaticLayout" POSITION="right" STYLE="bubble">
<stylenode LOCALIZED_TEXT="AutomaticLayout.level.root" COLOR="#000000" STYLE="oval" SHAPE_HORIZONTAL_MARGIN="10.0 pt" SHAPE_VERTICAL_MARGIN="10.0 pt">
<font SIZE="18"/>
</stylenode>
<stylenode LOCALIZED_TEXT="AutomaticLayout.level,1" COLOR="#0033ff">
<font SIZE="16"/>
<edge COLOR="#ff0000"/>
</stylenode>
<stylenode LOCALIZED_TEXT="AutomaticLayout.level,2" COLOR="#00b439">
<font SIZE="14"/>
<edge COLOR="#0000ff"/>
</stylenode>
<stylenode LOCALIZED_TEXT="AutomaticLayout.level,3" COLOR="#990000">
<font SIZE="12"/>
<edge COLOR="#00ff00"/>
</stylenode>
<stylenode LOCALIZED_TEXT="AutomaticLayout.level,4" COLOR="#111111">
<font SIZE="10"/>
<edge COLOR="#ff00ff"/>
</stylenode>
<stylenode LOCALIZED_TEXT="AutomaticLayout.level,5">
<edge COLOR="#00ffff"/>
</stylenode>
<stylenode LOCALIZED_TEXT="AutomaticLayout.level,6">
<edge COLOR="#7c0000"/>
</stylenode>
<stylenode LOCALIZED_TEXT="AutomaticLayout.level,7">
<edge COLOR="#00007c"/>
</stylenode>
<stylenode LOCALIZED_TEXT="AutomaticLayout.level,8">
<edge COLOR="#007c00"/>
</stylenode>
<stylenode LOCALIZED_TEXT="AutomaticLayout.level,9">
<edge COLOR="#7c007c"/>
</stylenode>
<stylenode LOCALIZED_TEXT="AutomaticLayout.level,10">
<edge COLOR="#007c7c"/>
</stylenode>
<stylenode LOCALIZED_TEXT="AutomaticLayout.level,11">
<edge COLOR="#7c7c00"/>
</stylenode>
</stylenode>
</stylenode>
</map_styles>
</hook>
<hook NAME="AutomaticEdgeColor" COUNTER="2" RULE="ON_BRANCH_CREATION"/>
<node TEXT="Repository" POSITION="right" ID="ID_1728251766" CREATED="1471355142365" MODIFIED="1471355147947">
<edge COLOR="#ff0000"/>
<node TEXT="Local" ID="ID_860804874" CREATED="1471355237328" MODIFIED="1471355242147">
<node TEXT="UNC repository (file system + XML)" ID="ID_1944640309" CREATED="1471355155503" MODIFIED="1471355189202"/>
<node TEXT="SQL repository" ID="ID_201899934" CREATED="1471355205521" MODIFIED="1471355212712"/>
</node>
<node TEXT="Remote via Web Services" ID="ID_305583392" CREATED="1471355243435" MODIFIED="1471355546865">
<node TEXT="SQL repository" ID="ID_1019850471" CREATED="1471355205521" MODIFIED="1471355212712"/>
<node TEXT="UNC repository (file system + XML)" ID="ID_630484704" CREATED="1471355155503" MODIFIED="1471355189202"/>
</node>
</node>
<node TEXT="UNC repository" POSITION="left" ID="ID_1895983380" CREATED="1471355581431" MODIFIED="1471355591225">
<edge COLOR="#0000ff"/>
<node TEXT="1. Root folder + sub-folders, each containing .QCM files" ID="ID_375539728" CREATED="1471355595540" MODIFIED="1471355633008"/>
<node TEXT="2. In each folder, a header.xml contains description for the folder content" ID="ID_832575900" CREATED="1471355638590" MODIFIED="1471355681432"/>
<node TEXT="3. Language and category settings are at the folder level and content (.QCM files) must match. While going deeper in the tree, the category can have a suffix (cat/subcat) : in this case, the .QCM files must match this category label." ID="ID_1175068204" CREATED="1471355765975" MODIFIED="1471356077788"/>
<node TEXT="4. Each .QCM file also has a header with name, description, language, category. Language and category must match the folder header." ID="ID_1489499082" CREATED="1471355837601" MODIFIED="1471356056441"/>
</node>
</node>
</map>
