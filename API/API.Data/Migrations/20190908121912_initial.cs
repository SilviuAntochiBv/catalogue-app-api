using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace API.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "classes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "teachers",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    first_name = table.Column<string>(maxLength: 100, nullable: false),
                    last_name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teachers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    first_name = table.Column<string>(maxLength: 100, nullable: false),
                    last_name = table.Column<string>(maxLength: 100, nullable: false),
                    age = table.Column<short>(nullable: false),
                    AssociatedClassId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.id);
                    table.ForeignKey(
                        name: "FK_students_classes_AssociatedClassId",
                        column: x => x.AssociatedClassId,
                        principalTable: "classes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true),
                    SubjectId = table.Column<int>(nullable: true),
                    TeacherId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => x.id);
                    table.ForeignKey(
                        name: "FK_courses_subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "subjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_courses_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "teachers_in_subjects",
                columns: table => new
                {
                    subject_id = table.Column<int>(nullable: false),
                    teacher_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teachers_in_subjects", x => new { x.subject_id, x.teacher_id });
                    table.ForeignKey(
                        name: "FK_teachers_in_subjects_subjects_subject_id",
                        column: x => x.subject_id,
                        principalTable: "subjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_teachers_in_subjects_teachers_teacher_id",
                        column: x => x.teacher_id,
                        principalTable: "teachers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "classes_in_course",
                columns: table => new
                {
                    class_id = table.Column<int>(nullable: false),
                    course_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classes_in_course", x => new { x.class_id, x.course_id });
                    table.ForeignKey(
                        name: "FK_classes_in_course_classes_class_id",
                        column: x => x.class_id,
                        principalTable: "classes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_classes_in_course_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "unique_class_name",
                table: "classes",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_classes_in_course_course_id",
                table: "classes_in_course",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "unique_course_name",
                table: "courses",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_courses_SubjectId",
                table: "courses",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_courses_TeacherId",
                table: "courses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_students_AssociatedClassId",
                table: "students",
                column: "AssociatedClassId");

            migrationBuilder.CreateIndex(
                name: "unique_subject_name",
                table: "subjects",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_teachers_in_subjects_teacher_id",
                table: "teachers_in_subjects",
                column: "teacher_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "classes_in_course");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "teachers_in_subjects");

            migrationBuilder.DropTable(
                name: "courses");

            migrationBuilder.DropTable(
                name: "classes");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "teachers");
        }
    }
}
