#include "cmd.h"

#include <QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    CMD w;
    w.show();
    return a.exec();
}
